using DSharpPlus;
using DSharpPlus.Entities;

namespace bot.commands_fh.Commands.Aredl.LinkAredl.Events.Interactions;

public static class OnLinkAccountInteraction
{
    public static void Register(EventHandlingBuilder builder)
    {
        builder.HandleComponentInteractionCreated(async (_, ev) =>
        {
            switch (ev.Id)
            {
                case "link-aredl:confirm":
                {
                    await ev.Interaction.CreateResponseAsync(
                        DiscordInteractionResponseType.UpdateMessage,
                        new DiscordInteractionResponseBuilder()
                            .WithContent("You have successfully confirmed the link."));
                    break;
                }
                case "link-aredl:cancel":
                {
                    await ev.Interaction.CreateResponseAsync(
                        DiscordInteractionResponseType.UpdateMessage,
                        new DiscordInteractionResponseBuilder()
                            .WithContent("You have successfully canceled the link request."));
                    break;
                }
            }
        });
    }
}