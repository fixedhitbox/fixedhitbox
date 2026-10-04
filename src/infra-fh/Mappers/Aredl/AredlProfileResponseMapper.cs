using application_fh.Dtos.External.Aredl;
using domain_fh.Common.Result;
using domain_fh.Common.Result.Errors;
using infra_fh.Network.External.Aredl.Dtos;

namespace infra_fh.Mappers.Aredl;

internal static class AredlProfileResponseMapper
{
    public static Result<AredlProfileDto, MapperError> Map(AredlProfileResponse? response)
    {
        if (response is null)
            return Result<AredlProfileDto, MapperError>.Failure(
                MapperError.NullObject(), "AredlProfileResponse is null.");

        if (string.IsNullOrWhiteSpace(response.Username))
            return Result<AredlProfileDto, MapperError>.Failure(
                MapperError.Validation(), "Username is missing.");
        if (response.DiscordId is null)
            return Result<AredlProfileDto, MapperError>.Failure(
                MapperError.Validation(), "DiscordId is missing.");
        if (response.CreatedInAredlAt is null)
            return Result<AredlProfileDto, MapperError>.Failure(
                MapperError.Validation(), "CreatedInAredlAt is missing.");

        var polishedRecords = new List<AredlRecordDto>();

        if (response.Records is not null)
        {
            foreach (var record in response.Records)
            {
                var result = AredlRecordResponseMapper.Map(record);
                
                if (!result.IsSuccess) continue;

                polishedRecords.Add(result.Value);
            }
        }
        
        var dto = new AredlProfileDto(
            Id: response.Id,
            Username: response.Username,
            GlobalName: response.GlobalName ?? response.Username,
            DiscordId: response.DiscordId.Value,
            Description: response.Description ?? string.Empty,
            Country: response.Country,
            CreatedAt: response.CreatedInAredlAt.Value,
            BackgroundLevel: response.BackgroundLevel,
            Records: polishedRecords);
        
        return Result<AredlProfileDto, MapperError>.Success(dto);
    }
}