using DSharpPlus.Commands.Processors.SlashCommands.Localization;

namespace bot.commands_fh.Commands.Diagnostics.Ping;

public class PingTranslator : IInteractionLocalizer
{
    public const string DefaultName = "ping";
    public const string DefaultDescription = "Pings the bot to check its latency.";

    public ValueTask<IReadOnlyDictionary<DiscordLocale, string>> TranslateAsync(string fullsymbolName)
        => fullsymbolName switch
        {
            $"{DefaultName}.description" => ValueTask.FromResult<IReadOnlyDictionary<DiscordLocale, string>>(
                new Dictionary<DiscordLocale, string>
                {
                    { DiscordLocale.en_US, DefaultDescription },
                    { DiscordLocale.pt_BR, "Verifica a latência do bot" },
                    { DiscordLocale.es_ES, "Hace sonar la bot para comprobar su latencia" }
                }),
            _ => ValueTask.FromResult<IReadOnlyDictionary<DiscordLocale, string>>(
                new Dictionary<DiscordLocale, string>())
        };
}