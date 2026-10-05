using application_fh.Dtos.External.Aredl;
using bot.commands_fh.Commands.Aredl.LinkAredl.Options;
using bot.commands_fh.Commands.Aredl.LinkAredl.Webhooks.Embeds;
using bot.commands_fh.Utils;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Entities;
using Microsoft.Extensions.Options;

namespace bot.commands_fh.Commands.Aredl.LinkAredl.Webhooks;

public static class PendingLinkWebhook
{
    public static DiscordWebhookBuilder Create(AredlProfileDto dto, SlashCommandContext sctx, IOptions<AredlAssetsOptions> assets)
    {
        var hardestRecord = dto.Records.FirstOrDefault();
        var hardestDemon = hardestRecord?.Level.Name;
        var videoUrl = hardestRecord?.VideoUrl;
        var countryPosition = dto.Rank.CountryRank;
        var levelRank = hardestRecord?.Level.Position;

        var embed = new DiscordEmbedBuilder()
            .WithTitle("🔗 | Link Confirmation")
            .WithDescription($"""
                              Hello {sctx.User.Mention}. I found your profile!

                              🔍 Does the information match your profile?
                              🪪 Confirm if the account link below is yours.
                              """)
            .AddField("Player", $"[{dto.GlobalName}](https://aredl.net/profile/user/{dto.Username})", true)
            .AddField("Country",
                $"{CountryCodeTranslator.IsoCodeToDiscordEmojiFlag(dto.Country)} #{countryPosition}", true)

            .AddField("Hardest",
                $"[<:extremedemon:1492859003070582865> {hardestDemon ?? "❓"}]({videoUrl}) #{levelRank}", true)
            
            .AddAredlBase(sctx, assets.Value.AredlBanner)
            .WithThumbnail(sctx.User.AvatarUrl);
        
        return new DiscordWebhookBuilder()
            .AddEmbed(embed)
            .AddActionRowComponent(PendingButtons());

    }
    
    private static DiscordButtonComponent[] PendingButtons()
    {
        DiscordButtonComponent[] buttons =
        [
            new (
                DiscordButtonStyle.Success,
                "link-aredl:confirm",
                "Yes. It's me!",
                false,
                new DiscordComponentEmoji("✅")),

            new (
                DiscordButtonStyle.Danger,
                "link-aredl:cancel",
                "No. Cancel",
                false,
                new DiscordComponentEmoji("✖️"))
        ];

        return buttons;
    }
}