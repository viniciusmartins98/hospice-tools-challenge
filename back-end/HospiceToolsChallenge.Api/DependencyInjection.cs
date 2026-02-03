using HospiceToolsChallenge.Api.Identity;
using HospiceToolsChallenge.Application;
using HospiceToolsChallenge.Infra;

namespace HospiceToolsChallenge.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration) {
            return services
                    .AddHttpContextAccessor()
                    .AddUserContext()
                    .AddApplication()
                    .AddInfra(configuration);
        }
    }
}
