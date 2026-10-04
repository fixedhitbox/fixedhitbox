using application_fh.Interfaces.Infrastructure.Aredl;
using infra_fh.Extensions.HttpClient;
using infra_fh.Extensions.Options;
using infra_fh.Network.External.Aredl.Services;
using infra_fh.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace infra_fh;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddAredlApiOptions(services, configuration);
        services.AddConfiguredHttpClient<IAredlApiService, AredlApiService, AredlApiOptions>();
        
        return services;
    }
    
    private static void AddAredlApiOptions (IServiceCollection services, IConfiguration configuration)
        => services.AddInfraAppOptions<AredlApiOptions>(configuration, options =>
        {
            options.Validate(o => !string.IsNullOrWhiteSpace(o.BaseUrl),
                "AredlApi Uri is required");

            options.Validate(o => o.TimeoutSeconds is >= 1 and <= 30,
                "AredlApi Timeout must be between 1 and 30 seconds");
        });
}