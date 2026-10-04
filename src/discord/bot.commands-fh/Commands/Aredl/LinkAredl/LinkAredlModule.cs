using bot.commands_fh.Contracts;
using bot.commands_fh.Contracts.Abstractions;

namespace bot.commands_fh.Commands.Aredl.LinkAredl;

public sealed class LinkAredlModule : IFeatureModule
{
    public FeatureInfo Info { get; } = new(
        "link-aredl",
        "Link your Discord account to your AREDL profile.",
        "Essencia");
    
    public IReadOnlyCollection<Type> Commands { get; } = [typeof(LinkAredlCommand)];
}