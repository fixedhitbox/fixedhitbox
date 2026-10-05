using bot.commands_fh.Contracts.Abstractions;
using bot.commands_fh.Policies;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace bot.main_fh.Features;

public static class DiscordFeatures
{
    public static void Register(
        IReadOnlyList<IFeatureModule> modules,
        ulong debugGuildId,
        IServiceCollection services)
    {
        services.ConfigureEventHandlers(events =>
        {
            foreach (var module in modules)
                 module.ConfigureEvents(events);
        });
        
        services.AddCommandsExtension((_, extension) =>
        {
            extension.AddProcessor(new SlashCommandProcessor(new SlashCommandConfiguration
                { NamingPolicy = new OrdinalKebabCaseInteractionNamingPolicy() }));

            extension.AddCommands(modules.SelectMany(m => m.Commands));

        }, new CommandsConfiguration
        {
            RegisterDefaultCommandProcessors = false,
            DebugGuildId = debugGuildId
        });
    }
}