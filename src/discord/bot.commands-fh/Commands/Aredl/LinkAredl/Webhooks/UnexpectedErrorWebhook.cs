using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Entities;

namespace bot.commands_fh.Commands.Aredl.LinkAredl.Webhooks;

public static class UnexpectedErrorWebhook
{
    private const string Description = 
        """
        :broken_heart: I'm sorry to interrupt your fun, but a small unexpected problem has prevented me from continuing.
        
        Please try again in a few moments.
        If the error persists, we would appreciate your help in better understanding what happened.
        
        """;
    public static DiscordWebhookBuilder Create(SlashCommandContext sctx, string? reason, string? referenceCode)
    {
        var embed = new DiscordEmbedBuilder()
            .WithColor(DiscordColor.Red)
            .WithTitle("😓🎈 | An unexpected error occurred");

        embed.WithDescription(!string.IsNullOrWhiteSpace(reason)
            ? $"**Reason:**\n> {reason}\n\n" + Description
            : Description);

        if (referenceCode is not null) 
            embed.WithFooter("Reference: " + referenceCode);
        
        return new DiscordWebhookBuilder()
            .WithContent(":boom: Internal failure in the system.")
            .AddEmbed(embed)
            .AddActionRowComponent(ActionButtons());
    }
    
    private static DiscordButtonComponent[] ActionButtons()
    {
        DiscordButtonComponent[] buttons =
        [
            
            new (
                DiscordButtonStyle.Primary,
                "faq_contribute_bugs",
                "How to contribute",
                false,
                new DiscordComponentEmoji("❓")),
            
            new (
                DiscordButtonStyle.Secondary,
                "github_repository",
                "Repository link for issues",
                emoji: new DiscordComponentEmoji("📁")),
        ];

        return buttons;
    }
}