using Microsoft.Extensions.DependencyInjection;
using HospiceToolsChallenge.Application.Repositories;

namespace HospiceToolsChallenge.Infra.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IPatientRepository, PatientRepository>()
                .AddTransient<IColorRepository, ColorRepository>()
                ;
        }
    }
}
