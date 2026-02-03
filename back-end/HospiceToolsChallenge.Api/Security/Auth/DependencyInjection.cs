using HospiceToolsChallenge.Api.Security.Auth.Jwt;

namespace HospiceToolsChallenge.Api.Security.Auth
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtBearerAuth(configuration);
            services.AddTransient<JwtHandler>();
            return services;
        }
    }
}
