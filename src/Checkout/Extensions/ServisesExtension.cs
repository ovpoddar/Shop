using Checkout.Builders;
using Checkout.Handlers;
using Checkout.Helpers;
using Checkout.Managers;
using Checkout.Services;
using Microsoft.Extensions.DependencyInjection;
using Shop.Models;

namespace Checkout.Extensions
{
    public static class ServisesExtension
    {
        public static IServiceCollection AddDescriptors(this IServiceCollection services)
        {
            services.AddTransient<IRequestBuilder, RequestBuilder>();

            services.AddTransient<ISentRequestService, SentRequestService>();

            services.AddTransient<IRequestManger, RequestManger>();
            services.AddTransient<IloginManager, loginManager>();
            services.AddTransient<IItemManager, ItemManager>();

            services.AddSingleton<IItemHandler<ItemModel>, ItemHandler>();
            services.AddTransient<IPurchaseHandler, PurchaseHandler>();
            services.AddSingleton<IUserHandler, UserHandler>();

            services.AddSingleton<IUserhelper, UserHelper>();
            return services;
        }
    }
}
