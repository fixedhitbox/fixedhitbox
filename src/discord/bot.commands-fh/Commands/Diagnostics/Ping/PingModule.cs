using bot.commands_fh.Contracts;
using bot.commands_fh.Contracts.Abstractions;
using Microsoft.Extensions.Logging;

namespace bot.commands_fh.Commands.Diagnostics.Ping;

public sealed class PingModule : IFeatureModule
{
    public FeatureInfo Info { get; } = new(
        "ping", "Check the bot's latency.", "Essencia");

    public IReadOnlyCollection<Type> Commands { get; } = [typeof(PingCommand)];
}