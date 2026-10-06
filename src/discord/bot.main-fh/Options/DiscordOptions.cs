using bot.main_fh.Options.Abstractions;

namespace bot.main_fh.Options;

public sealed class DiscordOptions : IAppOptions
{
    public string Token { get; init; } = string.Empty;
    public ulong? DebugGuildId { get; init; }
    public bool OverwriteGlobalApplicationCommands { get; init; } = false;
    
    public static string SectionName => "Discord";
}