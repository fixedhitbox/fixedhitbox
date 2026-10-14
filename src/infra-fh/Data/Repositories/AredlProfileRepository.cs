using domain_fh.Entities;
using domain_fh.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace infra_fh.Data.Repositories;

internal sealed class AredlProfileRepository(
    IDbContextFactory<AppDbContext> factory, 
    ILogger<AredlProfileRepository> logger) : IAredlProfileRepository
{
    public async Task<AredlProfile?> GetByDiscordIdAsync(ulong id, CancellationToken ct = default)
    {
        await using var db = await factory.CreateDbContextAsync(ct);
        
        return await ExecuteAsync(() => db.AredlProfiles.AsNoTracking()
            .FirstOrDefaultAsync(p => p.DiscordId == id, ct));
    }
    
    private async Task<T> ExecuteAsync<T>(Func<Task<T>> operation, string? errorMessage = null)
    {
        try { return await operation(); }
        
        catch (OperationCanceledException) { throw; }
        
        catch (Exception ex) when (ex is NpgsqlException || ex.InnerException is NpgsqlException)        
        {
            logger.LogError(ex, "{Error}", errorMessage ?? "Native database server error.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Error}", errorMessage ?? "An unhandled exception occurred during the execution of a query");
            throw;
        }
    }
}