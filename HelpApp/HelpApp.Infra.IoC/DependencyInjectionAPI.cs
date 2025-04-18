using HelpApp.Domain.Interfaces.Repositories;
using HelpApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HelpApp.Infra.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPI (this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
        
    
}