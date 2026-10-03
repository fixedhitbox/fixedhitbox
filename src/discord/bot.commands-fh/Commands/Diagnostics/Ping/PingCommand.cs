using System.ComponentModel;
using System.Diagnostics;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.SlashCommands.Localization;

namespace bot.commands_fh.Commands.Diagnostics.Ping;

public sealed class PingCommand
{
    [Command(PingTranslator.DefaultName), InteractionLocalizer<PingTranslator>]
    [Description(PingTranslator.DefaultDescription)]
    public static async Task ExecuteAsync(CommandContext ctx)
    {
        var sw = Stopwatch.StartNew();

        await ctx.RespondAsync("🏓 Measuring...");
        sw.Stop();

        var gatewayMs = ctx.Client.GetConnectionLatency(ctx.Guild?.Id ?? 0).TotalMilliseconds;
        var responseMs = sw.ElapsedMilliseconds;

        var content = gatewayMs > 0
            ? $"🏓 Gateway: {gatewayMs:F0}ms\n⏱️ Response: {responseMs:F0}ms."
            : $"🏓 Pong! Response: {responseMs:F0}ms";

        await ctx.EditResponseAsync(content);
    }
}