using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shop.ActionFilters;
using Shop.Handlers;
using Shop.Handlers.Interfaces;
using Shop.Helpers;
using Shop.Helpers.Interfaces;
using Shop.Managers;
using Shop.Managers.Interfaces;
using Shop.Utilities;
using System.Reflection;

namespace Shop.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<ProductWholeSale>, GenericRepositories<ProductWholeSale>>();
            services.AddScoped<IGenericRepository<WholesaleSize>, GenericRepositories<WholesaleSize>>();
            services.AddScoped<IGenericRepository<Employer>, GenericRepositories<Employer>>();
            services.AddScoped<IGenericRepository<Category>, GenericRepositories<Category>>();
            services.AddScoped<IGenericRepository<Product>, GenericRepositories<Product>>();
            services.AddScoped<IGenericRepository<Balance>, GenericRepositories<Balance>>();
            services.AddScoped<IGenericRepository<Brand>, GenericRepositories<Brand>>();
            services.AddScoped<IGenericRepository<Csv>, GenericRepositories<Csv>>();

            services.AddScoped<IProductRepositories, ProductRepositories>();

            services.AddTransient<ISuggestionHandler, SuggestionHandler>();
            services.AddTransient<IWholesaleHandler, WholesaleHandler>();
            services.AddTransient<ICategoryHandler, CategoryHandler>();
            services.AddTransient<IProductHandler, ProductHandler>();
            services.AddTransient<IBalanceHandler, BalanceHandler>();
            services.AddTransient<IBrandHandler, BrandHandler>();
            services.AddTransient<ICsvHandler, CsvHandler>();
            services.AddTransient<IProtectorHandler, ProtectorHandler>();
            services.AddTransient<IItemHandler, ItemHandler>();
            services.AddTransient<IEmployerHandler, EmployerHandler>();

            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<ICsvManager, CsvManager>();
            services.AddTransient<IBalanceManager, BalanceManager>();

            services.AddTransient<ICsvHelper, CsvHelper>();
            services.AddTransient<IProtectionHelper, ProtectionHelper>();
            services.AddTransient<IUserHelper, UserHelper>();


            services.AddTransient<IPaymentManager, PaymentManager>();
            services.AddTransient<IPaymentHandler, PaymentHandler>();
            return services;
        }

        public static IServiceCollection RegisterActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ProductActionFilter>();
            services.AddHttpClient();
            return services;
        }
    }
}
