using FluentValidation;
using HospiceToolsChallenge.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HospiceToolsChallenge.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(DependencyInjection));
            services
                .AddMediatR(config =>
                {
                    config.RegisterServicesFromAssembly(assembly);
                    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                })
                .AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
