namespace infra_fh.Options.Abstractions;

internal interface IHttpClientOptions
{
    string BaseUrl { get; set; }
    int TimeoutSeconds { get; set; }
}