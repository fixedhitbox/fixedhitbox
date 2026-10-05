using application_fh.Dtos.External.Aredl;
using domain_fh.Common.Result;
using domain_fh.Common.Result.Errors;
using infra_fh.Network.External.Aredl.Dtos;

namespace infra_fh.Mappers.Aredl;

internal static class AredlLevelResponseMapper
{
    public static Result<AredlLevelDto, MapperError> Map(AredlLevelResponse? response)
    {
        if (response is null)
            return Result<AredlLevelDto, MapperError>.Failure(MapperError.Validation(),
                "response.Level is null.");

        if (string.IsNullOrWhiteSpace(response.Name))
            return Result<AredlLevelDto, MapperError>.Failure(MapperError.Validation(),
                "response.Name is missing.");

        if (response.LevelId is null)
            return Result<AredlLevelDto, MapperError>.Failure(MapperError.Validation(),
                "response.Id is missing.");

        if (response.Position is null)
            return Result<AredlLevelDto, MapperError>.Failure(MapperError.Validation(),
                "response.Position is missing.");

        if (response.Points is null)
            return Result<AredlLevelDto, MapperError>.Failure(MapperError.Validation(),
                "response.Points is missing.");

        if (response.TwoPlayer is null)
            return Result<AredlLevelDto, MapperError>.Failure(MapperError.Validation(),
                "response.TwoPlayer is missing.");
        
        var recordResult = AredlRecordResponseMapper.Map(response.Records);

        if (!recordResult.IsSuccess)
            return Result<AredlLevelDto, MapperError>.Failure(MapperError.Validation(),
                recordResult.Message ?? "AredlRecordResponseMapper could not map a list of AredlRecordResponse.");
        
        return Result<AredlLevelDto, MapperError>.Success(new AredlLevelDto(
            response.Id,
            response.Name,
            response.LevelId.Value,
            response.TwoPlayer.Value,
            response.Position.Value,
            response.Points.Value,
            response.Description,
            response.Song,
            recordResult.Value));
    }
}