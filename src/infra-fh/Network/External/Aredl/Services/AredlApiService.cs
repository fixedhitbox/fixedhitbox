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
            return Result<AredlProfileDto, ApiError>.Failure(
                ApiError.CanceledOperation("AREDL.FETCH.CANCELLATION_REQUESTED"), ex.Message);
        }
        catch (TaskCanceledException ex)
        {
            logger.LogWarning(ex, "[AREDL.FETCH_TIMEOUT] AREDL request timed out for [{DiscordId}]", discordId);
            return Result<AredlProfileDto, ApiError>.Failure(ApiError.Timeout("AREDL.FETCH_TIMEOUT"), ex.Message);
        }
        catch (HttpRequestException ex)
        {
            logger.LogWarning(ex, "[AREDL.FETCH.EXTERNAL_SYSTEM_UNREACHABLE] AREDL unreachable for [{DiscordId}]", 
                discordId);
            return Result<AredlProfileDto, ApiError>.Failure(ApiError.Unexpected(
                "We couldn't reach AREDL right now.",
                "AREDL.FETCH.EXTERNAL_SYSTEM_UNREACHABLE"), ex.Message);
        }
        catch (Exception ex) when (ex is JsonException or NotSupportedException)
        {
            logger.LogError(ex, "[AREDL.FETCH.INVALID_DATA.001] Invalid AREDL response for [{DiscordId}]", discordId);
            return Result<AredlProfileDto, ApiError>.Failure(
                ApiError.InvalidResponse("AREDL.FETCH.INVALID_DATA.001"), ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[AREDL.FETCH.UNEXPECTED.001] Unexpected error fetching AREDL profile for [{DiscordId}]", 
                discordId);
            return Result<AredlProfileDto, ApiError>.Failure(ApiError.Unexpected(
                "It seems something strange happened in our system while " +
                "we were trying to find your profile on AREDL.",
                "AREDL.FETCH.UNEXPECTED.001"), 
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
                logger.LogWarning("[AREDL.FETCH.LIMITED_CONNECTION] AREDL rate limit hit. Retry-After: {RetryAfter}", retryAfter);

                return Result<AredlProfileDto, ApiError>.Failure(
                    ApiError.RateLimited(
                        "AREDL is receiving too many connections. Please try again in a moment.",
                        "AREDL.FETCH.LIMITED_CONNECTION"),
                    $"AREDL returned 429 for Discord ID {discordId}. Retry-After: {retryAfter}");
            }
            case System.Net.HttpStatusCode.NotFound:
                return Result<AredlProfileDto, ApiError>.Failure(
                    ApiError.NotFound("AREDL.FETCH.NOT_FOUND"), "Aredl profile user not found for: " + discordId);
        }

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("AREDL returned {StatusCode} for [{DiscordId}]", (int)response.StatusCode, discordId);

            return Result<AredlProfileDto, ApiError>.Failure(
                ApiError.Unexpected(
                    "[AREDL.FETCH.UNEXPECTED.002] Sorry, something went wrong while fetching your player profile.",
                    "AREDL.FETCH.UNEXPECTED.002"), 
                $"AREDL returned [{(int)response.StatusCode}] '{response.ReasonPhrase}' - for Discord ID {discordId}.");
        }
            
        var responseDto = await response.Content.ReadFromJsonAsync<AredlProfileResponse>(JsonOptions, ct);
        
        return EnsureValidResponseData(responseDto, discordId);
    }

    private Result<AredlProfileDto, ApiError> EnsureValidResponseData(AredlProfileResponse? responseDto, 
        ulong discordId)
    {
        if (responseDto is null)
        {
            logger.LogWarning("[AREDL.FETCH.INVALID_DATA.002] Aredl API sent an invalid or empty response for ID [{Id}]", 
                discordId);
            return Result<AredlProfileDto, ApiError>.Failure(
                ApiError.InvalidResponse("AREDL.FETCH.INVALID_DATA.002"), 
                "Aredl API sent an invalid or empty response.");
        }

        var map = AredlProfileResponseMapper.Map(responseDto);

        if (!map.IsSuccess)
        {
            logger.LogError("[AREDL.FETCH.INVALID_DATA.003] {Mapper} could not map a profile in {Service} for [{Id}] - {Reason}",
                nameof(AredlProfileResponseMapper), 
                nameof(AredlApiService), 
                discordId,
                map.Message);
            
            return Result<AredlProfileDto, ApiError>.Failure(
                ApiError.InvalidResponse("AREDL.FETCH.INVALID_DATA.003"),
                map.Message ??
                $"{nameof(AredlProfileResponseMapper)} could not map a profile in {nameof(AredlApiService)}.");
        }

        if (string.IsNullOrWhiteSpace(map.Value.Username))
        {
            logger.LogError("[AREDL.FETCH.INVALID_DATA.004] Aredl API returned an object with an empty username for [{Id}].",
                discordId);
            return Result<AredlProfileDto, ApiError>.Failure(ApiError.NotFound("AREDL.FETCH.INVALID_DATA.004"),
                "Aredl API returned an object with an empty username.");
        }
        
        return Result<AredlProfileDto, ApiError>.Success(map.Value);
    }
}