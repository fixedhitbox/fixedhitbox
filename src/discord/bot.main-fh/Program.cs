using application_fh;
using bot.main_fh;
using bot.main_fh.Extensions.Options;
using bot.main_fh.Extensions.Services.DiscordBot;
using infra_fh;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

return await AppStartupGuard.TryRunConsoleApplicationAsync(async () =>
{
    Console.WriteLine($"""
                      ███████╗██╗██╗  ██╗███████╗██████╗ ██╗  ██╗██╗████████╗██████╗  ██████╗ ██╗  ██╗
                      ██╔════╝██║╚██╗██╔╝██╔════╝██╔══██╗██║  ██║██║╚══██╔══╝██╔══██╗██╔═══██╗╚██╗██╔╝
                      █████╗  ██║ ╚███╔╝ █████╗  ██║  ██║███████║██║   ██║   ██████╔╝██║   ██║ ╚███╔╝ 
                      ██╔══╝  ██║ ██╔██╗ ██╔══╝  ██║  ██║██╔══██║██║   ██║   ██╔══██╗██║   ██║ ██╔██╗ 
                      ██║     ██║██╔╝ ██╗███████╗██████╔╝██║  ██║██║   ██║   ██████╔╝╚██████╔╝██╔╝ ██╗
                      ╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝╚═════╝ ╚═╝  ╚═╝╚═╝   ╚═╝   ╚═════╝  ╚═════╝ ╚═╝  ╚═╝
                      {VersionControl.GetVersion()} | essenciaftw, rochakauan, rigan (C) 2026
                      https://pureessence.dev/hosted-services/discord/fixedhitbox
                      Powered by DSharpPlus 5.x (.NET 10)
                      
                      Licensed under GNU Affero General Public License v3.0. This program comes with ABSOLUTELY NO WARRANTY.
                      Source code: https://github.com/fixedhitbox/fixedhitbox
                      
                      Everyone is permitted to copy and distribute verbatim copies
                      of this license document, but changing it is not allowed.
                      ---
                      """);
    
    Log.Information("Attempting to initialize the application...");

    var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
    { Args = args, ContentRootPath = AppContext.BaseDirectory });

    builder.Configuration
        .AddJsonFile("botsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"botsettings.{builder.Environment.EnvironmentName}.json",
            optional: true, reloadOnChange: true);
    
    builder.Services.AddSerilog((services, loggerConfig) => loggerConfig
        .ReadFrom.Configuration(builder.Configuration)
        .ReadFrom.Services(services));
    
    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddApplication();
    
    builder.Services.AddDiscordOptions(builder.Configuration);
    builder.Services.AddDiscordBot(builder.Configuration, builder.Configuration.GetValue<ulong>("Discord:DebugGuildId"));

    var host = builder.Build();
    Log.Information("Environment: {Env}.", builder.Environment.EnvironmentName);

    try
    {
        await host.RunAsync();
    }
    
    catch (NullReferenceException) when (Environment.ExitCode is not 0)
    { 
        // DSharpPlus throws an NRE when disposing of a DiscordClient that never connected.
        Log.Debug("Ignoring NullReferenceException in DiscordClient's Dispose after initialization failure.");
    }
    
    catch (Exception ex) when (ex is OperationCanceledException or HostAbortedException)
    {
        Log.Information("Application shutdown initialized via terminal/host.");
    }

    return Environment.ExitCode;
});