using Application.Interfaces.Persistence;
using Application.Interfaces.Services;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            var assembly = typeof(ApplicationDbContext).Assembly.FullName;

            services.AddDbContext<ApplicationDbContext>(
                optionsAction => 
                optionsAction.UseSqlServer(configurationManager.GetConnectionString("ConectionDatabase"), x => x.MigrationsAssembly(assembly))            
            );

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var infraAsm = Assembly.GetExecutingAssembly();
            foreach (var impl in infraAsm.GetTypes()
                         .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository")))
            {
                var @interface = impl.GetInterfaces()
                    .FirstOrDefault(i => i.Name == "I" + impl.Name);
                if (@interface != null)
                    services.AddScoped(@interface, impl);
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IOrderingQuery, OrderingQuery>();

            return services;
        }

    }
}
