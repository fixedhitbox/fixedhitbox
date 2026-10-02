using application_fh.Dtos.External.Aredl;
using domain_fh.Common.Result;
using domain_fh.Common.Result.Errors;
using infra_fh.Network.External.Aredl.Dtos;

namespace infra_fh.Mappers.Aredl;

internal static class AredlRecordResponseMapper
{
    public static Result<AredlRecordDto, MapperError> Map(AredlRecordResponse? response)
    {
        if (response is null)
            return Result<AredlRecordDto, MapperError>.Failure(MapperError.NullObject(),
                "AredlRecordResponse is null");

        if (response.Mobile is null)
            return Result<AredlRecordDto, MapperError>.Failure(MapperError.Validation(), 
                "response.Mobile is missing.");
        if (response.IsVerification is null)
            return Result<AredlRecordDto, MapperError>.Failure(MapperError.Validation(),
                "response.IsVerification is missing.");
        if (response.CreatedAt is null)
            return Result<AredlRecordDto, MapperError>.Failure(MapperError.Validation(),
                "response.CreatedAt is missing.");

        var levelResult = AredlLevelResponseMapper.Map(response.Level);
        if (!levelResult.IsSuccess)
            return Result<AredlRecordDto, MapperError>.Failure(MapperError.Validation(),
                levelResult.Message ?? "AredlLevelResponseMapper could not map an AredlLevelResponse.");

        return Result<AredlRecordDto, MapperError>.Success(new AredlRecordDto(
            response.Id,
            response.Mobile.Value,
            response.VideoUrl,
            response.IsVerification.Value,
            response.CreatedAt.Value,
            response.UpdatedAt,
            levelResult.Value
        ));
    }

    public static Result<IReadOnlyList<AredlRecordDto>, MapperError> Map(IReadOnlyList<AredlRecordResponse>? response)
    {
        var polishedList = new List<AredlRecordDto>();
        
        if (response is null)
            return Result<IReadOnlyList<AredlRecordDto>, MapperError>.Failure(MapperError.NullObject(),
                "AredlRecordResponse is null");

        foreach (var mappingResult in response.Select(Map))
        {
            if (!mappingResult.IsSuccess)
                return Result<IReadOnlyList<AredlRecordDto>, MapperError>.Failure(MapperError.Validation(),
                    mappingResult.Message ?? "AredlRecordResponseMapper could not map a list of AredlRecordResponse.");
            
            polishedList.Add(mappingResult.Value);
        }
        
        return Result<IReadOnlyList<AredlRecordDto>, MapperError>.Success(polishedList);
    }
}