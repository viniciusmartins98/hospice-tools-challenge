using HospiceToolsChallenge.Application.UserContext;

namespace HospiceToolsChallenge.Api.Identity
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserContext(this IServiceCollection services)
        {
            return services.AddTransient<IUserContext, UserContext>();
        }
    }
}
