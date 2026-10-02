using infra_fh.Options.Abstractions;

namespace infra_fh.Options;

internal sealed class AredlApiOptions : IHttpClientOptions, IInfraOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 5;
    
    public static string SectionName => "AredlApi";
}