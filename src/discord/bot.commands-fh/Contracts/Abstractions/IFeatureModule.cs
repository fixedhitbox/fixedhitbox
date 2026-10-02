using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace bot.commands_fh.Contracts.Abstractions;

public interface IFeatureModule
{
    FeatureInfo Info { get; }

    IReadOnlyCollection<Type> Commands => [];

    void ConfigureServices(IServiceCollection services) { }
    void ConfigureEvents(EventHandlingBuilder events) { }
    
    void OnEnable(ILogger  logger) { }
    void OnDisable(ILogger logger) { }
}