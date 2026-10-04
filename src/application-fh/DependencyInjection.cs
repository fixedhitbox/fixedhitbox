using application_fh.Interfaces.Application.Aredl;
using application_fh.UseCases.Aredl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace application_fh;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
        => services.AddTransient<IStartLinkAredl, StartLinkAredl>();
}