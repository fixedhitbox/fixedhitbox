using domain_fh.Exceptions;
using infra_fh.Options.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace infra_fh.Extensions.Options;

internal static class OptionsExtensions
{
    public static void AddInfraAppOptions<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<OptionsBuilder<T>>? configure = null)
        where T : class, IInfraOptions
    {
        var section = configuration.GetSection(T.SectionName);

        if (!section.Exists())
            throw new ConfigurationSectionNotFoundException(T.SectionName, typeof(T));
        
        var builder = services.AddOptions<T>()
            .Bind(configuration.GetSection(T.SectionName))
            .ValidateOnStart();
        
        configure?.Invoke(builder);
    }
}