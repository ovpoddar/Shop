using DataAccess.Entities;
using DataAccess.Helpers;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public static class AllDataAccessDependency
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<ProductWholeSale>, GenericRepositories<ProductWholeSale>>();
            services.AddScoped<IGenericRepository<WholesaleSize>, GenericRepositories<WholesaleSize>>();
            services.AddScoped<IGenericRepository<Category>, GenericRepositories<Category>>();
            services.AddScoped<IGenericRepository<Product>, GenericRepositories<Product>>();
            services.AddScoped<IGenericRepository<Balance>, GenericRepositories<Balance>>();
            services.AddScoped<IGenericRepository<Brand>, GenericRepositories<Brand>>();
            services.AddScoped<IGenericRepository<Csv>, GenericRepositories<Csv>>();

            services.AddScoped<IProductRepositories, ProductRepositories>();

            services.AddScoped<ICatagoriesHelper, CatagoriesHelper>();
            return services;
        }
}
}
