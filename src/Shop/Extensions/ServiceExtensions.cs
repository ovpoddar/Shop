using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shop.ActionFilters;
using Shop.Handlers;
using Shop.Handlers.Interfaces;
using Shop.Helpers;
using Shop.Helpers.Interfaces;
using Shop.Managers;
using Shop.Managers.Interfaces;

namespace Shop.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<ProductWholeSale>, GenericRepositories<ProductWholeSale>>();
            services.AddScoped<IGenericRepository<WholesaleSize>, GenericRepositories<WholesaleSize>>();
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
            services.AddTransient<ITokenHandler, TokenHandler>();

            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<ICsvManager, CsvManager>();
            services.AddTransient<IBalanceManager, BalanceManager>();
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();

            services.AddTransient<ICsvHelper, CsvHelper>();


            services.AddTransient<IPaymentManager, PaymentManager>();
            services.AddTransient<IPaymentHandler, PaymentHandler>();
            return services;
        }

        public static IServiceCollection RegisterActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ProductActionFilter>();
            return services;
        }
    }
}
