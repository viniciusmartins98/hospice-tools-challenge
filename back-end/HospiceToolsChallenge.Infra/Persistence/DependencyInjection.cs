using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HospiceToolsChallenge.Infra.Persistence {
    public static class DependencyInjection {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));
            return services;
        }
    }
}
