using bot.commands_fh.Commands.Aredl.LinkAredl.Options;
using bot.commands_fh.Commands.Aredl.LinkAredl.Webhooks.Embeds;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Entities;
using Microsoft.Extensions.Options;

namespace bot.commands_fh.Commands.Aredl.LinkAredl.Webhooks;

public static class NotFoundWebhook
{
    public static DiscordWebhookBuilder Create(SlashCommandContext sctx, IOptions<AredlAssetsOptions> assets)
    {
        var errorEmbed = new DiscordEmbedBuilder()
            .WithTitle("💢 | It seems like you don't have an AREDL account.")
            .WithDescription("You need to have an account on AREDL (All Rated Extreme Demons List) to make the link.")
            .AddField("<:aredl:1493905107559907379> AREDL website", "https://aredl.net/",
                inline: true)
            .AddAredlBase(sctx, assets.Value.AredlBanner);
        
        return new DiscordWebhookBuilder()
            .AddEmbed(errorEmbed)
            .AddActionRowComponent(ActionButtons());
    }
    
    private static DiscordButtonComponent[] ActionButtons()
    {
        DiscordButtonComponent[] buttons =
        [
            
            new (
                DiscordButtonStyle.Secondary,
                "link-aredl:faq_whats-aredl",
                "What is AREDL",
                false,
                new DiscordComponentEmoji("❓")),
            
            new (
                DiscordButtonStyle.Secondary,
                "link-aredl:faq_aredl-link",
                "How does this work",
                emoji: new DiscordComponentEmoji("❓")),
        ];

        return buttons;
    }
}