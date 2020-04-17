using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Data;
using Shop.Extensions;
using Shop.Uilities;

namespace Shop
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment webHostEnvironment)
        {
            var appSettings = $"appsettings.{webHostEnvironment.EnvironmentName}.json";

            var builder = new ConfigurationBuilder()
                .SetBasePath(webHostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile(appSettings, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContextPool<ApplicationDbContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("database")));

            services.AddControllersWithViews();
            services.AddDependencies();
            services.RegisterActionFilters();

            services.AddSingleton(
                new MapperConfiguration(e => { e.AddProfile(new AppProfileMapping()); }).CreateMapper());
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
                applicationBuilder.UseDeveloperExceptionPage();
            else
                applicationBuilder.UseExceptionHandler("/Error");

            InitializeDb(applicationBuilder);

            applicationBuilder.UseCookiePolicy();
            applicationBuilder.UseStaticFiles();
            applicationBuilder.UseRouting();
            applicationBuilder.UseEndpoints(endpoints =>
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Product}/{action=index}/{id?}"));
        }

        private static void InitializeDb(IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
        }
    }
}
