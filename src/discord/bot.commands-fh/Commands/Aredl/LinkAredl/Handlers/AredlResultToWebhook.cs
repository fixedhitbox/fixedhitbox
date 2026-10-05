using application_fh.Dtos.External.Aredl;
using bot.commands_fh.Commands.Aredl.LinkAredl.Options;
using bot.commands_fh.Commands.Aredl.LinkAredl.Webhooks;
using domain_fh.Common.Result;
using domain_fh.Common.Result.Errors;
using domain_fh.Enums;
using DSharpPlus.Commands.Processors.SlashCommands;
using Microsoft.Extensions.Options;

namespace bot.commands_fh.Commands.Aredl.LinkAredl.Handlers;

public static class AredlResultToWebhook
{
    public static async Task Handle(
        SlashCommandContext sctx, 
        Result<AredlProfileDto, ApiError> result, 
        IOptions<AredlAssetsOptions> assets)
    {
        if (result.IsSuccess)
        {
            await sctx.EditResponseAsync(PendingLinkWebhook.Create(result.Value, sctx, assets));
            return;
        }

        switch (result.Error!.Category)
        {
            case EErrorCategory.CanceledOperation:
                await sctx.EditResponseAsync(":octagonal_sign: Operation canceled by you. Nothing changed.");
                break;
            case EErrorCategory.Timeout:
                await sctx.EditResponseAsync("""
                                              ⚠️ | **There was a connection problem!**
                                             The AREDL system appears to be offline or has taken too long to respond.
                                             *Please try again in a few minutes.*
                                             """);
                break;
            case EErrorCategory.NotFound:
                await sctx.EditResponseAsync(NotFoundWebhook.Create(sctx, assets));
                break;
            default:
                await sctx.EditResponseAsync(UnexpectedErrorWebhook
                    .Create(sctx, result.Error.Message, result.Error.ErrorReferenceCode));
                break;
        }
    }
}