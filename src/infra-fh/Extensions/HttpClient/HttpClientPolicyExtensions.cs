using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace infra_fh.Extensions.HttpClient;

internal static class HttpClientPolicyExtensions
{
    public static void AddStandardPolicies(this IHttpClientBuilder builder)
        => builder.AddPolicyHandler(RetryPolicy());
    
    private static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retry =>
                TimeSpan.FromMilliseconds(200 * retry));
}