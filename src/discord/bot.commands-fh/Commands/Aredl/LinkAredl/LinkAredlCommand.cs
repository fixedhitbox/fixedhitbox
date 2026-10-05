using System.ComponentModel;
using application_fh.Interfaces.Application.Aredl;
using bot.commands_fh.Commands.Aredl.LinkAredl.Handlers;
using bot.commands_fh.Commands.Aredl.LinkAredl.Options;
using bot.commands_fh.Commands.Aredl.LinkAredl.Webhooks;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Commands.Processors.SlashCommands.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace bot.commands_fh.Commands.Aredl.LinkAredl;

public sealed class LinkAredlCommand(IOptions<AredlAssetsOptions> assets)
{
    [Command(LinkAredlTranslator.DefaultName), InteractionLocalizer<LinkAredlTranslator>]
    [Description(LinkAredlTranslator.DefaultDescription)]
    public async Task ExecuteAsync(SlashCommandContext sctx)
    {
        await sctx.RespondAsync("🚀 Connecting to AREDL...", ephemeral: true);

        var useCase = sctx.ServiceProvider.GetRequiredService<IStartLinkAredl>();
        
        var result = await useCase.FetchAsync(sctx.User.Id);
        
        await AredlResultToWebhook.Handle(sctx, result, assets);
    }
}