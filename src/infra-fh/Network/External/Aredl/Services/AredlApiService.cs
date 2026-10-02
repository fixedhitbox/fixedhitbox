using System.Net.Http.Json;
using System.Text.Json;
using application_fh.Dtos.External.Aredl;
using application_fh.Interfaces.Infrastructure.Aredl;
using domain_fh.Common.Result;
using domain_fh.Common.Result.Errors;
using infra_fh.Mappers.Aredl;
using infra_fh.Network.External.Aredl.Dtos;
using Microsoft.Extensions.Logging;

namespace infra_fh.Network.External.Aredl.Services;

internal sealed class AredlApiService(HttpClient httpClient, ILogger<AredlApiService> logger) : IAredlApiService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true
    };

    public async Task<Result<AredlProfileDto, ApiError>> FetchProfileAsync(ulong discordId,
        CancellationToken ct = default)
    {
        try
        {
            using var response = await httpClient.GetAsync($"v2/api/aredl/profile/{discordId}", ct);
            return await HandleApiResponses(response, discordId, ct);
        }
        catch (OperationCanceledException ex) when (ct.IsCancellationRequested)
        {
            return Result<AredlProfileDto, ApiError>.Failure(ApiError.CanceledOperation(), ex.Message);
        }
        catch (TaskCanceledException ex)
        {
            logger.LogWarning(ex, "AREDL request timed out for {DiscordId}", discordId);
            return Result<AredlProfileDto, ApiError>.Failure(ApiError.Timeout(), ex.Message);
        }
        catch (HttpRequestException ex)
        {
            logger.LogWarning(ex, "AREDL unreachable for {DiscordId}", discordId);
            return Result<AredlProfileDto, ApiError>.Failure(ApiError.Unexpected(
                "We couldn't reach AREDL right now."), ex.Message);
        }
        catch (Exception ex) when (ex is JsonException or NotSupportedException)
        {
            logger.LogError(ex, "Invalid AREDL response for {DiscordId}", discordId);
            return Result<AredlProfileDto, ApiError>.Failure(ApiError.InvalidResponse(), ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error fetching AREDL profile for {DiscordId}", discordId);
            return Result<AredlProfileDto, ApiError>.Failure(ApiError.Unexpected(
                "It seems something strange happened in our system while " +
                "we were trying to find your profile on AREDL."), 
                ex.Message);
        }
    }

    private async Task<Result<AredlProfileDto, ApiError>> HandleApiResponses(
        HttpResponseMessage response, ulong discordId, CancellationToken ct)
    {
        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.TooManyRequests:
            {
                var retryAfter = response.Headers.RetryAfter?.Delta
                                 ?? (response.Headers.RetryAfter?.Date - DateTimeOffset.UtcNow);
                logger.LogWarning("AREDL rate limit hit. Retry-After: {RetryAfter}", retryAfter);

                return Result<AredlProfileDto, ApiError>.Failure(
                    ApiError.RateLimited("AREDL is receiving too many connections. Please try again in a moment."),
                    $"AREDL returned 429 for Discord ID {discordId}. Retry-After: {retryAfter}");
            }
            case System.Net.HttpStatusCode.NotFound:
                return Result<AredlProfileDto, ApiError>.Failure(
                    ApiError.NotFound(), "Aredl profile user not found for: " + discordId);
        }

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("AREDL returned {StatusCode} for {DiscordId}", (int)response.StatusCode, discordId);

            return Result<AredlProfileDto, ApiError>.Failure(
                ApiError.Unexpected("Sorry, something went wrong while fetching your player profile."), 
                $"AREDL returned [{(int)response.StatusCode}] '{response.ReasonPhrase}' - for Discord ID {discordId}.");
        }
            
        var responseDto = await response.Content.ReadFromJsonAsync<AredlProfileResponse>(JsonOptions, ct);
        
        return EnsureValidResponseData(responseDto);
    }

    private static Result<AredlProfileDto, ApiError> EnsureValidResponseData(AredlProfileResponse? responseDto)
    {
        if (responseDto is null)
            return Result<AredlProfileDto, ApiError>.Failure(
                ApiError.InvalidResponse(), "Aredl API sent an invalid or empty response.");

        var map = AredlProfileResponseMapper.Map(responseDto);

        if (!map.IsSuccess)
            return Result<AredlProfileDto, ApiError>.Failure(
                ApiError.InvalidResponse(),
                map.Message ??
                $"{nameof(AredlProfileResponseMapper)} could not map a profile in {nameof(AredlApiService)}.");

        if (string.IsNullOrWhiteSpace(map.Value.Username))
            return Result<AredlProfileDto, ApiError>.Failure(ApiError.NotFound(),
                "Aredl API returned an object with an empty username.");
        
        return Result<AredlProfileDto, ApiError>.Success(map.Value);
    }
}