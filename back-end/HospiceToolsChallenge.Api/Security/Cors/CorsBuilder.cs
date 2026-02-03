using Microsoft.AspNetCore.Cors.Infrastructure;

namespace HospiceToolsChallenge.Api.Security.Cors
{
    public static class CorsBuilder
    {
        /// <summary>Adds cross-origin resource sharing services to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.</summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add services to.</param>
        /// <param name="configuration">Configuration class that provide Cors settings.</param>
        /// <returns>The CorsOptions whit CorsPolicyBuilder definded.</returns>
        public static CorsOptions? AddCors(this IServiceCollection services, IConfiguration configuration)
        {

            IConfigurationSection section = configuration.GetSection("Cors");
            if (section == null)
            {
                return default;
            }

            var config = section.Get<CorsConfiguration>();
            if (config == null)
            {
                return default;
            }

            // Setup configuration when informed
            var corsOptions = new CorsOptions();

            void corsBuilder(CorsPolicyBuilder builder)
            {
                // Set headers Cors settings
                if (config.Headers?.Length > 0)
                {
                    builder.WithHeaders(config.Headers);
                }
                else
                {
                    builder.AllowAnyHeader();
                }

                // Set verbs Cors settings
                if (config.Methods?.Length > 0)
                {
                    builder.WithMethods(config.Methods);
                }
                else
                {
                    builder.AllowAnyMethod();
                }

                // Set origins Cors settings
                if (config.Origins?.Length > 0)
                {
                    builder.WithOrigins(config.Origins);
                }

                // Set exposed headers Cors settings
                if (config.ExposedHeaders?.Length > 0)
                {
                    builder.WithExposedHeaders(config.ExposedHeaders);
                }
            }

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(corsBuilder);
                corsOptions = options;
            });

            return corsOptions;
        }
    }
}
