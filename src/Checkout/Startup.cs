using Checkout.Builders;
using Checkout.Handlers;
using Checkout.Managers;
using Checkout.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Models;

namespace Checkout
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<IRequestBuilder, RequestBuilder>();
            services.AddTransient<ISentRequestService, SentRequestService>();
            services.AddTransient<IRequestManger, RequestManger>();
            services.AddTransient<IItemManager, ItemManager>();
            services.AddTransient<IPurchaseHandler, PurchaseHandler>();
            services.AddSingleton<IItemHandler<ItemModel>, ItemHandler>();
            services.AddHttpClient();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Checkout}/{action=index}/{id?}");
            });
        }
    }
}
