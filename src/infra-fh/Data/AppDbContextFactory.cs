using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace infra_fh.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    // dotnet ef migrations add MigrationTitle --project infra-fh --startup-project discord/bot.main-fh
    // dotnet ef database update --project infra-fh --startup-project discord/bot.main-fh
    public AppDbContext CreateDbContext(string[] args)
    {
        var current = Directory.GetCurrentDirectory();
        var botPath = Path.Combine(current, "discord", "bot.main-fh");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.Exists(botPath) ? botPath : current)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .AddUserSecrets("4658c121-df18-437d-a1d1-767aff5ce00e")
            .Build();

        var connectionString = configuration["AppOptions:ConnectionString"]
                               ?? throw new InvalidOperationException("AppOptions:ConnectionString not found.");

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new AppDbContext(options);
    }
}