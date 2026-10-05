using bot.commands_fh.Commands.Aredl.LinkAredl.Events.Interactions;
using bot.commands_fh.Commands.Aredl.LinkAredl.Options;
using bot.commands_fh.Contracts;
using bot.commands_fh.Contracts.Abstractions;
using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace bot.commands_fh.Commands.Aredl.LinkAredl;

public sealed class LinkAredlModule : IFeatureModule
{
    public FeatureInfo Info { get; } = new(
        "link-aredl",
        "Link your Discord account to your AREDL profile.",
        "Essencia");

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOptions<AredlAssetsOptions>()
            .BindConfiguration(AredlAssetsOptions.Section)
            .ValidateOnStart();
    }

    public void ConfigureEvents(EventHandlingBuilder events)
    {
        OnLinkAccountInteraction.Register(events);
        FaqInteractions.Register(events);
    }

    public void OnEnable(ILogger logger)
    {
        if (logger.IsEnabled(LogLevel.Debug)) 
            logger.LogDebug("- Binding configuration from {AssetsOptions} to {Class}", 
            AredlAssetsOptions.Section, nameof(AredlAssetsOptions));
    }
    public IReadOnlyCollection<Type> Commands { get; } = [typeof(LinkAredlCommand)];
}