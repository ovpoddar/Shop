using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Entities;
using Shop.Handlers;
using Shop.Helpers;
using Shop.Managers;
using Shop.Repositories;
using Shop.Uilities;

namespace Shop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContextPool<ApplicationDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("database")));
            services.AddTransient<IGenericRepository<ProductWholeSale>, GenericRepositories<ProductWholeSale>>();
            services.AddTransient<IGenericRepository<WholesaleSize>, GenericRepositories<WholesaleSize>>();
            services.AddTransient<IGenericRepository<Category>, GenericRepositories<Category>>();
            services.AddTransient<IGenericRepository<Product>, GenericRepositories<Product>>();
            services.AddTransient<IGenericRepository<Balance>, GenericRepositories<Balance>>();
            services.AddTransient<IGenericRepository<Brand>, GenericRepositories<Brand>>();
            services.AddTransient<IGenericRepository<Csv>, GenericRepositories<Csv>>();

            services.AddTransient<IProductRepositories, ProductRepositories>();

            services.AddTransient<ISuggestionHandler, SuggestionHandler>();
            services.AddTransient<IWholesaleHandler, WholesaleHandler>();
            services.AddTransient<ICategoryHandler, CategoryHandler>();
            services.AddTransient<IProductHandler, ProductHandler>();
            services.AddTransient<IBalanceHandler, BalanceHandler>();
            services.AddTransient<IBrandHandler, BrandHandler>();
            services.AddTransient<ICsvHandler, CsvHandler>();

            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<IPaymentManager, PaymentManager>();
            services.AddTransient<ICsvManager, CsvManager>();
            services.AddTransient<IBalanceManager, BalanceManager>();

            services.AddTransient<ICsvHelper, CsvHelper>();
            services.AddMvc();
            services.AddSingleton(new MapperConfiguration(e =>
            {
                e.AddProfile(new AppProfileMapping());
            }).CreateMapper());
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllerRoute(name: "default",pattern: "{controller=Product}/{action=index}/{id?}"));
        }
    }
}
