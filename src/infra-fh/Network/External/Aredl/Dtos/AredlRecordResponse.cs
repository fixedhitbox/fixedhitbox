using System.Text.Json.Serialization;

namespace infra_fh.Network.External.Aredl.Dtos;

internal sealed class AredlRecordResponse
{
    public Guid Id { get; init; }
    
    [JsonPropertyName("level")]
    public AredlLevelResponse? Level { get; init; }

    [JsonPropertyName("mobile")]
    public bool? Mobile { get; init; }
    public string? VideoUrl { get; init; }
    public bool? IsVerification { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}