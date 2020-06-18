using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shop.Extensions;
using Shop.Utilities;

namespace Shop
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new System.ArgumentNullException(nameof(_configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.HttpOnly = HttpOnlyPolicy.None;
                options.Secure = CookieSecurePolicy.SameAsRequest;
                options.OnAppendCookie = cookieContext => cookieContext.CheckSameSite();
                options.OnDeleteCookie = cookieContext => cookieContext.CheckSameSite();
            });


            // it will return the name of the project
            var migrationsAssembly = typeof(Startup).Namespace;

            services.AddDbContext<ApplicationDbContext>(builder =>
                builder.UseSqlServer(_configuration.GetConnectionString("database"),
                    sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)
                    ));

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddHttpContextAccessor();
            services.AddDependencies();
            services.RegisterActionFilters();
            services.AddCors(options => options.AddPolicy("All", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            services.AddSingleton(
                new MapperConfiguration(e => { e.AddProfile(new AppProfileMapping()); }).CreateMapper());
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop", Version = "v1" }); });
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
                applicationBuilder.UseDeveloperExceptionPage();
            else
                applicationBuilder.UseExceptionHandler("/Error");


            InitializeDb(applicationBuilder);

            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop");
            });
            applicationBuilder.UseCookiePolicy();
            applicationBuilder.UseStaticFiles();
            applicationBuilder.UseRouting();
            applicationBuilder.UseCors();
            applicationBuilder.UseEndpoints(endpoints =>
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Product}/{action=index}/{id?}"));
        }

        private static void InitializeDb(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
        }
    }
}
