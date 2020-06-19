using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Checkout.Builders;
using Checkout.Services;
using Checkout.Managers;
using Checkout.Handlers;
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
            services.AddSingleton<IItemHandler<ItemModel>, ItemHandler>();
            services.AddSingleton<IPaymentManager, PaymentManager>();
            services.AddSingleton<IPaymentHandler, PaymentHandler>();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Checkout}/{action=index}/{id?}");
            });
        }
    }
}
