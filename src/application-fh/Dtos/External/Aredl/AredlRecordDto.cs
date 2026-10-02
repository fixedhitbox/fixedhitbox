namespace application_fh.Dtos.External.Aredl;

public record AredlRecordDto(
    Guid Id,
    bool Mobile,
    string? VideoUrl,
    bool IsVerification,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt,
    AredlLevelDto Level,
    IReadOnlyList<AredlProfileDto>? Players = null
    );