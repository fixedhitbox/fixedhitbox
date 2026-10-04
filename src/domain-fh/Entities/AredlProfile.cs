using domain_fh.Entities.Abstractions;

namespace domain_fh.Entities;

public sealed class AredlProfile : Entity
{
    public ulong DiscordId { get; set; }
    
    public string Username { get; set; } = string.Empty;
    public string GlobalName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Guid AredlUserId { get; set; }
    public int? Country { get; set; }
    public DateTimeOffset CreatedInAredlAt { get; set; }

    public DateTimeOffset LinkedAtUtc { get; private set; }
    public DateTimeOffset LastUpdatedAt { get; private set; }
    
    private AredlProfile() { }

    public AredlProfile(
        ulong discordId,
        string username,
        string globalName,
        string description,
        Guid aredlUserId,
        int? country)
    {
        DiscordId = discordId;
        Username = username;
        GlobalName = globalName;
        Description = description;
        AredlUserId = aredlUserId;
        Country = country;
    }
}