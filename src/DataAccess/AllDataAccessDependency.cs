using DataAccess.Entities;
using DataAccess.Helpers;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class AllDataAccessDependency
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositories<>));

            services.AddScoped<IProductRepositories, ProductRepositories>();

            services.AddScoped<ICatagoriesHelper, CatagoriesHelper>();
            return services;
        }
    }
}
