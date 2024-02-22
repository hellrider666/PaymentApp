using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PaymentApp.Application.Classes.Behaviors;
using PaymentApp.Application.Classes.Mapping;
using System.Reflection;

namespace PaymentApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));

                cfg.AddProfile(new AssemblyMappingProfile(typeof(IPipelineBehavior<,>).Assembly));
            });

            services
                .AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBihavior<,>));

            return services;
        }
    }
}
