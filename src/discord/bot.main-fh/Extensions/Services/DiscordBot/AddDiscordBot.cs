using bot.commands_fh.Commands.Diagnostics.Ping;
using bot.main_fh.Extensions.FeatureModule;
using bot.main_fh.Features;
using bot.main_fh.HostedServices;
using DSharpPlus;
using DSharpPlus.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bot.main_fh.Extensions.Services.DiscordBot;

internal static class DiscordBotDependencyInjection
{
    public static IServiceCollection AddDiscordBot(this IServiceCollection services,
        IConfiguration configuration,
        ulong debugGuildId = 0)
    {
        var token = configuration.GetValue<string>("Discord:Token");
        if (string.IsNullOrEmpty(token))
            throw new ArgumentException("Discord Bot Token is missing in the configuration file");
        
        var modules = services.AddFeatureModules(typeof(PingModule).Assembly);
        DiscordFeatures.Register(modules, debugGuildId, services);
        
        services.AddDiscordClient(token, DiscordIntents.AllUnprivileged);
        services.AddHostedService<DiscordBotService>();
        return services;
    }
}