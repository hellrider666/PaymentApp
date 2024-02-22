using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Persistence.Classes.Context;
using PaymentApp.Persistence.Classes.Repositories;

namespace PaymentApp.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IAppDbContext, AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.InjectRepositories();

            return services;
        }

        private static IServiceCollection InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            return services;
        }
    }
}
