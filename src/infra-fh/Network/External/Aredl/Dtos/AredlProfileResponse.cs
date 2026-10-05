using System.Text.Json.Serialization;

namespace infra_fh.Network.External.Aredl.Dtos;

internal sealed record AredlProfileResponse
{
    public Guid Id { get; init; }
    public string? Username { get; init; }
    public string? GlobalName { get; init; }

    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public ulong? DiscordId { get; init; }

    public string? Description { get; init; }

    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? Country { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedInAredlAt { get; init; }
    
    [JsonPropertyName("background_level")]
    public int? BackgroundLevel { get; init; }
    
    [JsonPropertyName("rank")]
    public AredlProfileRankResponse? Rank { get; init; }
    
    [JsonPropertyName("records")]
    public IReadOnlyList<AredlRecordResponse>? Records { get; init; } = [];
}