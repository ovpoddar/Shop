using Microsoft.Extensions.DependencyInjection;
using Shop.ActionFilters;
using Shop.Handlers;
using Shop.Handlers.Interfaces;
using Shop.Helpers;
using Shop.Helpers.Interfaces;
using Shop.Managers;
using Shop.Managers.Interfaces;
using Shop.Wrappers;
using Shop.Wrappers.Interfaces;

namespace Shop.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<IBalanceManager, BalanceManager>();
            services.AddTransient<IPaymentManager, PaymentManager>();
            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<ICsvManager, CsvManager>();

            services.AddTransient<ISuggestionHandler, SuggestionHandler>();
            services.AddTransient<IProtectorHandler, ProtectorHandler>();
            services.AddTransient<IWholesaleHandler, WholesaleHandler>();
            services.AddTransient<ICategoryHandler, CategoryHandler>();
            services.AddTransient<IEmployerHandler, EmployerHandler>();
            services.AddTransient<IBalanceHandler, BalanceHandler>();
            services.AddTransient<IPaymentHandler, PaymentHandler>();
            services.AddTransient<IProductHandler, ProductHandler>();
            services.AddTransient<IBrandHandler, BrandHandler>();
            services.AddTransient<ITokenHandler, TokenHandler>();
            services.AddTransient<IItemHandler, ItemHandler>();
            services.AddTransient<ICsvHandler, CsvHandler>();

            services.AddTransient<ICsvHelper, CsvHelper>();

            services.AddTransient<ISignInManagerWrapper, SignInManagerWrapper>();
            services.AddTransient<IUserManagerWrapper, UserManagerWrapper>();

            return services;
        }

        public static IServiceCollection RegisterActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ProductActionFilter>();
            return services;
        }
    }
}
