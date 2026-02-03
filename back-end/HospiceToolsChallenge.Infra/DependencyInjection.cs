using HospiceToolsChallenge.Infra.Persistence;
using HospiceToolsChallenge.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HospiceToolsChallenge.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            return services.AddRepositories()
                .AddDatabase(configuration);
        }
    }
}
