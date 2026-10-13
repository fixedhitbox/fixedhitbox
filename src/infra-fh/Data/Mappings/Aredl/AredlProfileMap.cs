using domain_fh.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infra_fh.Data.Mappings.Aredl;

public sealed class AredlProfileMap : IEntityTypeConfiguration<AredlProfile>
{
    public void Configure(EntityTypeBuilder<AredlProfile> builder)
    {
        builder.ToTable("aredl_profile");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        
        builder.Property(e => e.PublicId)
            .HasColumnName("public_id")
            .IsRequired()
            .HasDefaultValueSql("uuidv7()");
        
        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(e => e.ModifiedAt)
            .HasColumnName("modified_at");


        builder.Property(p => p.DiscordId)
            .HasColumnName("discord_id")
            .HasConversion(v => unchecked((long)v), v => unchecked((ulong)v))
            .IsRequired();
        
        builder.Property(p => p.Username)
            .HasColumnName("username")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.GlobalName)
            .HasColumnName("global_name")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(300)
            .IsRequired();
        
        builder.Property(p => p.AredlUserId)
            .HasColumnName("aredl_user_id")
            .IsRequired();

        builder.Property(p => p.Country).HasColumnName("country");
        
        builder.Property(p => p.CreatedInAredlAt)
            .HasColumnName("created_in_aredl_at")
            .IsRequired();
        
        builder.Property(p => p.LinkedAtUtc)
            .HasColumnName("linked_at_utc")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(p => p.LastSyncedAt)
            .HasColumnName("last_synced_at")
            .IsRequired()
            .HasDefaultValueSql("now()");
        
        builder.HasIndex(e => e.PublicId).IsUnique();
        builder.HasIndex(p => p.DiscordId).IsUnique();
        builder.HasIndex(p => p.AredlUserId).IsUnique();
        builder.HasIndex(p => p.Country);
    }
}