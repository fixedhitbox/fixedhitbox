using DSharpPlus;
using DSharpPlus.Entities;

namespace bot.commands_fh.Commands.Aredl.LinkAredl.Events.Interactions;

public static class FaqInteractions
{
    public static void Register(EventHandlingBuilder builder)
    {
        builder.HandleComponentInteractionCreated(async (_, ev) =>
        {
            switch (ev.Id)
            {
                case "link-aredl:faq_whats-aredl":
                {
                    await ev.Interaction.CreateResponseAsync(
                        DiscordInteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder()
                            .WithContent("The All Rated Extreme Demons List (AREDL) is a community based on ranking every " +
                                         "rated Extreme Demon in Geometry Dash as accurately as we can.")
                            .AsEphemeral());
                    break;
                }
                case "link-aredl:faq_aredl-link":
                {
                    await ev.Interaction.CreateResponseAsync(
                        DiscordInteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder()
                            .WithContent("I dont know")
                            .AsEphemeral());
                    break;
                }
            }
        });
    }
}