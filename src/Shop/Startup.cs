using AutoMapper;
using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shop.Extensions;
using Shop.Utilities;
using System;
using System.Reflection;
using System.Text;

namespace Shop
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _configuration.GetSection("Jwt")["Issuer"],
                        ValidAudience = _configuration.GetSection("Jwt")["Audiences"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_configuration.GetSection("Jwt")["secret"]))
                    };
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.HttpOnly = HttpOnlyPolicy.None;
                options.Secure = CookieSecurePolicy.SameAsRequest;
                options.OnAppendCookie = cookieContext => cookieContext.CheckSameSite();
                options.OnDeleteCookie = cookieContext => cookieContext.CheckSameSite();
            });
            services.AddDbContext<ApplicationDbContext>(builder => builder.UseSqlServer(_configuration.GetConnectionString("database"), sqlOptions => sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddHttpContextAccessor();
            services.AddDependencies();
            services.RegisterActionFilters();
            services.AddCors(options => options.AddPolicy("All", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            services.AddSingleton(
                new MapperConfiguration(e => { e.AddProfile(new AppProfileMapping()); }).CreateMapper());
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop", Version = "v1" }); });
            services.AddIdentity<Employer, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(cookie =>
            {
                cookie.LoginPath = "/Authentication/LogIn";
                cookie.AccessDeniedPath = "/Product/MissingPage";
            });
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment, IServiceProvider serviceProvider)
        {
            if (webHostEnvironment.IsDevelopment())
                applicationBuilder.UseDeveloperExceptionPage();
            else
                applicationBuilder.UseExceptionHandler("/Error");
            serviceProvider.GetService<ApplicationDbContext>()
                .Database.Migrate();
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop");
            });
            applicationBuilder.UseStaticFiles();
            applicationBuilder.UseRouting();
            applicationBuilder.UseCors();
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseEndpoints(endpoints =>
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Product}/{action=index}/{id?}"));
        }
    }
}
