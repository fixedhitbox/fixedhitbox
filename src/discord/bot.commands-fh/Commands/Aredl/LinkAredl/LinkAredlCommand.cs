using System.ComponentModel;
using application_fh.Interfaces.Application.Aredl;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Commands.Processors.SlashCommands.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace bot.commands_fh.Commands.Aredl.LinkAredl;

public sealed class LinkAredlCommand
{
    [Command(LinkAredlTranslator.DefaultName), InteractionLocalizer<LinkAredlTranslator>]
    [Description(LinkAredlTranslator.DefaultDescription)]
    public static async Task ExecuteAsync(SlashCommandContext sctx)
    {
        await sctx.RespondAsync("🚀 Connecting to AREDL...", ephemeral: true);

        var useCase = sctx.ServiceProvider.GetRequiredService<IStartLinkAredl>();
        
        var result = await useCase.FetchAsync(sctx.User.Id);
        
        await sctx.EditResponseAsync($"{result.Value.GlobalName}: {result.Value.Description}");
    }
}