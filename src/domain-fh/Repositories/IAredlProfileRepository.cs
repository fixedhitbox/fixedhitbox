using domain_fh.Entities;

namespace domain_fh.Repositories;

public interface IAredlProfileRepository
{
    Task<AredlProfile?> GetByDiscordIdAsync(ulong id, CancellationToken ct = default);
}