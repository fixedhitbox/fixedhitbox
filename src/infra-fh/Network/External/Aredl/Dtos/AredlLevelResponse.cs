using System.Text.Json.Serialization;

namespace infra_fh.Network.External.Aredl.Dtos;

internal sealed class AredlLevelResponse
{
    public Guid Id { get; init; }
    
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? Position { get; init; }
    public string? Name { get; init; }
    
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? Points { get; init; }
    public bool? Legacy { get; init; }

    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public ulong? LevelId { get; init; }
    public bool? TwoPlayer { get; init; }
    
    //public List<string>? Tags { get; set; } Later...
    
    public string? Description { get; init; }
    
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int? Song { get; init; }
    
    public IReadOnlyList<AredlRecordResponse>? Records { get; init; } = [];
    
}