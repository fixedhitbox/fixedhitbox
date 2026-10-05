namespace application_fh.Dtos.External.Aredl;

public record AredlLevelDto(
    Guid Id,
    string Name,
    ulong LevelId,
    bool TwoPlayer,
    int Position,
    int Points,
    string? Description,
    int? Song,
    IReadOnlyList<AredlRecordDto>? Records = null);