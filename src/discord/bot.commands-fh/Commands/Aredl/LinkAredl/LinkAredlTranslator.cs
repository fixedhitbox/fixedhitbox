using DSharpPlus.Commands.Processors.SlashCommands.Localization;

namespace bot.commands_fh.Commands.Aredl.LinkAredl;

public class LinkAredlTranslator : IInteractionLocalizer
{
    public const string DefaultName = "link-aredl";
    public const string DefaultDescription = "Link your Discord account to your AREDL profile.";

    public ValueTask<IReadOnlyDictionary<DiscordLocale, string>> TranslateAsync(string fullsymbolName)
        => fullsymbolName switch
        {
            $"{DefaultName}.name" => ValueTask.FromResult<IReadOnlyDictionary<DiscordLocale, string>>(
                new Dictionary<DiscordLocale, string>
                {
                    { DiscordLocale.en_US, DefaultName },
                    { DiscordLocale.pt_BR, "vincular-aredl" },
                    { DiscordLocale.es_ES, "vincular-aredl" }
                }),
            
            $"{DefaultName}.description" => ValueTask.FromResult<IReadOnlyDictionary<DiscordLocale, string>>(
                new Dictionary<DiscordLocale, string>
                {
                    { DiscordLocale.en_US, DefaultDescription },
                    { DiscordLocale.pt_BR, "Vincula sua conta Discord com seu perfil da AREDL." },
                    { DiscordLocale.es_ES, "Vincula tu cuenta de Discord a tu perfil de AREDL." }
                }),
            _ => ValueTask.FromResult<IReadOnlyDictionary<DiscordLocale, string>>(
                new Dictionary<DiscordLocale, string>())
        };
}