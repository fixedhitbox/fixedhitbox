using System.Text.Json.Serialization;

namespace infra_fh.Network.External.Aredl.Dtos;

internal sealed class AredlProfileRankResponse
{
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? Rank { get; init; }
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? RawRank { get; init; }
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? ExtremesRank { get; init; }
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? HardestRank { get; init; }
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? CountryRank { get; init; }
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? CountryRawRank { get; init; }
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? CountryExtremesRank { get; init; }
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? CountryHardestRank { get; init; }
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? TotalPoints { get; init; }
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? PackPoints { get; init; }
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? Extremes { get; init; }
}