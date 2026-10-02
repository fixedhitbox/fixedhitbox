using System.Reflection;
using bot.commands_fh.Contracts.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace bot.main_fh.Extensions.FeatureModule;

public static class FeaturesLoader
{
    public static IReadOnlyList<IFeatureModule> AddFeatureModules(
        this IServiceCollection services, Assembly assembly)
    {
        var modules = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false }
                        && typeof(IFeatureModule).IsAssignableFrom(t))
            .Select(t => (IFeatureModule)Activator.CreateInstance(t)!)
            .ToList();

        foreach (var module in modules)
        {
            module.ConfigureServices(services);
            services.AddSingleton(module);
        }
        
        return modules;
    }
}