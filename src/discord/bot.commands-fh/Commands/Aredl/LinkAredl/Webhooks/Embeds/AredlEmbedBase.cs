using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Entities;

namespace bot.commands_fh.Commands.Aredl.LinkAredl.Webhooks.Embeds;

public static class AredlEmbedBase
{
    private static readonly DiscordColor DefaultColor = DiscordColor.Yellow;

    public static DiscordEmbedBuilder AddAredlBase(
        this DiscordEmbedBuilder builder,
        SlashCommandContext sctx,
        string? bannerUrl,
        DiscordColor? color = null)
    {
        builder
            .WithColor(color ?? DefaultColor)
            .WithTimestamp(DateTimeOffset.UtcNow)
            .WithFooter("Fixed Hitbox - bot • AREDL Integration", sctx.Client.CurrentUser.AvatarUrl);

        return bannerUrl is null ? builder : builder.WithImageUrl(bannerUrl);
    }
}