using bot.commands_fh.Contracts.Abstractions;
using bot.main_fh.Options;
using DSharpPlus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace bot.main_fh.HostedServices;

internal sealed class DiscordBotService(
    DiscordClient client,
    IOptions<DiscordOptions> options,
    IHostApplicationLifetime lifetime,
    IEnumerable<IFeatureModule> modules,
    ILoggerFactory loggerFactory,
    ILogger<DiscordBotService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            if (options.Value.OverwriteGlobalApplicationCommands)
                await OverrideCommandsAsync();
            
            EnableModules();
            
            await client.ConnectAsync();

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Connected to Discord! Bot is now online. Debug guild id is {Debug}", options.Value.DebugGuildId);
            
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        
        catch (DSharpPlus.Exceptions.UnauthorizedException)
        {
            logger.LogCritical("The request to Discord API was not authorized. Check if the token provided is valid.");
            Environment.ExitCode = 1;
            lifetime.StopApplication();
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Shutdown signal received.");
        }
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Shutting down discord bot.");
        
        foreach (var module in modules)
        {
            try { module.OnDisable(loggerFactory.CreateLogger($"Feature.{module.Info.Name}")); }
            catch (Exception ex) { logger.LogError(ex, "Feature {Name} failed during OnDisable.", module.Info.Name); }
        }
        
        return base.StopAsync(stoppingToken);
    }

    private void EnableModules()
    {
        foreach (var module in modules)
        {
            var moduleLogger = loggerFactory.CreateLogger($"Feature.{module.Info.Name}");
            
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug(
                    "Enabled feature {Name} by {Author}: {Description} ({CommandCount} command(s))",
                    module.Info.Name, module.Info.Author, module.Info.Description, module.Commands.Count);

            try
            {
                module.OnEnable(moduleLogger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Feature {Name} failed during OnEnable.", module.Info.Name);
            }
        }
    }
    private async Task OverrideCommandsAsync()
    {
        logger.LogWarning("{SectionName} is ACTIVE. Global commands were just wiped. " +
                          "Set this to false in botsettings.json now, or it will run again on every restart.",
            nameof(options.Value.OverwriteGlobalApplicationCommands));

        await client.BulkOverwriteGlobalApplicationCommandsAsync([]);

        if (options.Value.DebugGuildId is { } guildId)
        {
            await client.BulkOverwriteGuildApplicationCommandsAsync(guildId, []);
            logger.LogWarning("CLEARED both global and guild commands. Please restart the bot!");
        }
        else logger.LogWarning("CLEARED global commands. Please restart the bot!");
    }
}