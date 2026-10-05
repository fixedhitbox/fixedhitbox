namespace application_fh.Dtos.External.Aredl;

public sealed record AredlProfileRankDto(
    int Rank,
    int RawRank,
    int ExtremesRank,
    int HardestRank,
    int CountryRank,
    int CountryRawRank,
    int CountryExtremesRank,
    int CountryHardestRank,
    int TotalPoints,
    int PackPoints,
    int Extremes
    );