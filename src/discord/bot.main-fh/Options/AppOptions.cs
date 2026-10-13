using bot.main_fh.Options.Abstractions;

namespace bot.main_fh.Options;

internal sealed class AppOptions : IAppOptions
{
    public string ConnectionString { get; init; } = string.Empty;
    
    public static string SectionName => "AppOptions";
}