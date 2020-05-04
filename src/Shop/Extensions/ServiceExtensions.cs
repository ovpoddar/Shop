using Shop.Builders;
using Shop.Manager;
using Shop.Services;
using Microsoft.Extensions.DependencyInjection;
using Shop.ActionFilters;
using Shop.Entities;
using Shop.Handlers;
using Shop.Helpers;
using Shop.Managers;
using Shop.Models;
using Shop.Repositories;

namespace Shop.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<IGenericRepository<ProductWholeSale>, GenericRepositories<ProductWholeSale>>();
            services.AddTransient<IGenericRepository<WholesaleSize>, GenericRepositories<WholesaleSize>>();
            services.AddTransient<IGenericRepository<Category>, GenericRepositories<Category>>();
            services.AddTransient<IGenericRepository<Product>, GenericRepositories<Product>>();
            services.AddTransient<IGenericRepository<Balance>, GenericRepositories<Balance>>();
            services.AddTransient<IGenericRepository<Brand>, GenericRepositories<Brand>>();
            services.AddTransient<IGenericRepository<Csv>, GenericRepositories<Csv>>();

            services.AddTransient<IProductRepositories, ProductRepositories>();

            services.AddTransient<ISuggestionHandler, SuggestionHandler>();
            services.AddSingleton<IItemHandler<ItemModel>, ItemHandler>();
            services.AddTransient<IWholesaleHandler, WholesaleHandler>();
            services.AddTransient<ICategoryHandler, CategoryHandler>();
            services.AddTransient<IProductHandler, ProductHandler>();
            services.AddTransient<IBalanceHandler, BalanceHandler>();
            services.AddTransient<IBrandHandler, BrandHandler>();
            services.AddTransient<ICsvHandler, CsvHandler>();

            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<IRequestManger, RequestManger>();
           // services.AddTransient<IPaymentManager, PaymentManager>();
            services.AddTransient<ICsvManager, CsvManager>();
            services.AddTransient<IBalanceManager, BalanceManager>();
            services.AddTransient<IItemManager, ItemManager>();

            services.AddTransient<IRequestBuilder, RequestBuilder>();

            services.AddTransient<ISentRequestService, SentRequestService>();

            services.AddTransient<ICsvHelper, CsvHelper>();

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
