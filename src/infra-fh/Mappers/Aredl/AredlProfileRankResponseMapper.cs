using application_fh.Dtos.External.Aredl;
using domain_fh.Common.Result;
using domain_fh.Common.Result.Errors;
using infra_fh.Network.External.Aredl.Dtos;

namespace infra_fh.Mappers.Aredl;

internal static class AredlProfileRankResponseMapper
{
    public static Result<AredlProfileRankDto, MapperError> Map(AredlProfileRankResponse? response)
{
    if (response is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.NullObject(),
            $"{nameof(AredlProfileRankResponse)} is null.");

    if (response.Rank is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.Validation(),
            $"{nameof(AredlProfileRankDto.Rank)} is missing.");

    if (response.RawRank is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.Validation(),
            $"{nameof(AredlProfileRankDto.RawRank)} is missing.");

    if (response.ExtremesRank is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.Validation(),
            $"{nameof(AredlProfileRankDto.ExtremesRank)} is missing.");

    if (response.HardestRank is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.Validation(),
            $"{nameof(AredlProfileRankDto.HardestRank)} is missing.");

    if (response.CountryRank is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.Validation(),
            $"{nameof(AredlProfileRankDto.CountryRank)} is missing.");

    if (response.CountryRawRank is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.Validation(),
            $"{nameof(AredlProfileRankDto.CountryRawRank)} is missing.");

    if (response.CountryExtremesRank is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.Validation(),
            $"{nameof(AredlProfileRankDto.CountryExtremesRank)} is missing.");

    if (response.CountryHardestRank is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.Validation(),
            $"{nameof(AredlProfileRankDto.CountryHardestRank)} is missing.");

    if (response.TotalPoints is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.Validation(),
            $"{nameof(AredlProfileRankDto.TotalPoints)} is missing.");

    if (response.PackPoints is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.Validation(),
            $"{nameof(AredlProfileRankDto.PackPoints)} is missing.");

    if (response.Extremes is null)
        return Result<AredlProfileRankDto, MapperError>.Failure(MapperError.Validation(),
            $"{nameof(AredlProfileRankDto.Extremes)} is missing.");

    var dto = new AredlProfileRankDto(
        Rank: response.Rank.Value,
        RawRank: response.RawRank.Value,
        ExtremesRank: response.ExtremesRank.Value,
        HardestRank: response.HardestRank.Value,
        CountryRank: response.CountryRank.Value,
        CountryRawRank: response.CountryRawRank.Value,
        CountryExtremesRank: response.CountryExtremesRank.Value,
        CountryHardestRank: response.CountryHardestRank.Value,
        TotalPoints: response.TotalPoints.Value,
        PackPoints: response.PackPoints.Value,
        Extremes: response.Extremes.Value);

    return Result<AredlProfileRankDto, MapperError>.Success(dto);
}
}