namespace application_fh.Dtos.External.Aredl;

public record AredlProfileDto(
    Guid Id,
    string Username,
    string GlobalName,
    ulong DiscordId,
    string Description,
    int? Country,
    DateTimeOffset CreatedAt,
    int? BackgroundLevel,
    IReadOnlyList<AredlRecordDto> Records);