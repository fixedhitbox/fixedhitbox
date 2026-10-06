using bot.main_fh.Options;
using bot.main_fh.Options.Abstractions;
using domain_fh.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace bot.main_fh.Extensions.Options;

internal static class OptionsExtensions
{
    extension(IServiceCollection services)
    {
        private OptionsBuilder<T> AddConfiguredOptions<T>(
            IConfiguration configuration, 
            Action<OptionsBuilder<T>>? configure = null)
            where T : class, IAppOptions
            => services.AddConfiguredOptions<T>(configuration, T.SectionName, configure);

        private OptionsBuilder<T> AddConfiguredOptions<T>(
            IConfiguration configuration, string sectionName,
            Action<OptionsBuilder<T>>? configure = null)
            where T : class, IAppOptions
        {
            var section = configuration.GetSection(sectionName);
        
            if (!section.Exists())
                throw new ConfigurationSectionNotFoundException(T.SectionName, typeof(T));
        
            var builder = services.AddOptions<T>()
                .Bind(configuration.GetRequiredSection(sectionName))
                .ValidateOnStart();
        
            configure?.Invoke(builder);
        
            return builder;
        }

        public IServiceCollection AddAppOptions(IConfiguration configuration)
        {
            services.AddConfiguredOptions<AppOptions>(configuration, options => 
                options.Validate(o => !string.IsNullOrWhiteSpace(o.ConnectionString),
                    "Connection string is required. Ensure you have provided a valid connection string."));
            
            return services;
        }
        
        public void AddDiscordOptions(IConfiguration configuration)
            => services.AddConfiguredOptions<DiscordOptions>(configuration, options =>
            {
                options.Validate(o => !string.IsNullOrWhiteSpace(o.Token),
                    "Discord token is required.");

                options.Validate(o => o.Token.Length <= 100,
                    "Discord token is too long. Ensure you have provided a valid token.");

                options.Validate(o =>
                    {
                        if (o.DebugGuildId is null) return true;

                        return o.DebugGuildId > 10;
                    },
                    "Discord debug guild id must be between 1 and 100 characters.");
            });
    }
}