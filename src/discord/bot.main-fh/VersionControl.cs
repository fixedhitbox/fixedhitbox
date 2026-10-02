using System.Reflection;

namespace bot.main_fh;

internal static class VersionControl
{
    public static string GetVersion()
    {
        var info = Assembly.GetEntryAssembly()!
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? "unknown version";

        var plus = info.IndexOf('+');
        if (plus >= 0 && info.Length > plus + 8)
            info = info[..(plus + 8)];
        
        return info;
    }
}