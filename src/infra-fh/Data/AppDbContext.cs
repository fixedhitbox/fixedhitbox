using domain_fh.Entities;
using domain_fh.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace infra_fh.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<AredlProfile> AredlProfiles => Set<AredlProfile>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ChangeTracker.DetectChanges();
        ApplyTimestamps();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess, CancellationToken ct = default)
    {
        ChangeTracker.DetectChanges();
        ApplyTimestamps();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, ct);
    }
    
    private void ApplyTimestamps()
    { 
        var now = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries<Entity>())
            if (entry.State is EntityState.Modified)
                entry.Property(entity => entity.ModifiedAt).CurrentValue = now;
    }
}