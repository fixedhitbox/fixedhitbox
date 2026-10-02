using infra_fh.Options.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace infra_fh.Extensions.HttpClient;

internal static class HttpClientExtensions
{
    public static void AddConfiguredHttpClient<TClient, TImplementation, TOptions>
        (this IServiceCollection services)
            where TClient : class
            where TImplementation : class, TClient
            where TOptions : class, IHttpClientOptions
    {
        services.AddHttpClient<TClient, TImplementation>()
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<TOptions>>()
                    .Value;

                client.BaseAddress = new Uri(options.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
            })
            .AddStandardPolicies();
    }
}