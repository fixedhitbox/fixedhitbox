using domain_fh.Entities.Abstractions;

namespace domain_fh.Entities;

public sealed class AredlProfile : Entity
{
    public ulong DiscordId { get; private set; }
    
    public string Username { get; private set; } = string.Empty;
    public string GlobalName { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public Guid AredlUserId { get; private set; }
    public int? Country { get; private set; }
    public DateTimeOffset CreatedInAredlAt { get; private set; }

    public DateTimeOffset LinkedAtUtc { get; private set; }
    public DateTimeOffset LastSyncedAt { get; private set; }
    
    private AredlProfile() { }

    public AredlProfile(
        ulong discordId,
        string username,
        string globalName,
        string description,
        Guid aredlUserId,
        int? country,
        DateTimeOffset createdInAredlAt)
    {
        DiscordId = discordId;
        Username = username;
        GlobalName = globalName;
        Description = description;
        AredlUserId = aredlUserId;
        Country = country;
        CreatedInAredlAt = createdInAredlAt.ToUniversalTime();
        
        var now = DateTimeOffset.UtcNow;
        LinkedAtUtc = now;
        LastSyncedAt = now;
    }
}