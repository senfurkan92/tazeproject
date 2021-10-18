using BLL.Data.Manager;
using BLL.Data.Service;
using DAL.Context;
using DAL.Data.Abstract;
using DAL.Data.Concrete;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Extentions
{
    public static class ExtentionManagement
    {
        /// <summary>
        /// dependencyInjection yapilarinin kurulmasi
        /// </summary>
        /// <param name="services"></param>
        public static void AddDiConfig(this IServiceCollection services)
        {
            services.AddTransient<IDal_Category, Dal_Category>();
            services.AddTransient<IService_Category, Manager_Category>();
            services.AddTransient<IDal_Article, Dal_Article>();
            services.AddTransient<IService_Article, Manager_Article>();
        }

        /// <summary>
        /// entityframeworkidentity ve cookie konfigurasyonu
        /// </summary>
        /// <param name="services"></param>
        public static void AddIdentityConfig(this IServiceCollection services)
        {
            // dbcontext
            services.AddDbContext<ProjectDbContext>();

            // identity config
            services.AddIdentity<AppUser, AppRole>(config => {
                // password
                config.Password.RequireDigit = true;
                config.Password.RequiredLength = 8;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = true;
                config.Password.RequireNonAlphanumeric = false;
                // user
                config.User.RequireUniqueEmail = true;
                // lockout
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                config.Lockout.MaxFailedAccessAttempts = 5;
            }).AddEntityFrameworkStores<ProjectDbContext>().AddDefaultTokenProviders();

            // cookie build
            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Name = "TazeAuth";
            cookieBuilder.HttpOnly = true;
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            cookieBuilder.SameSite = SameSiteMode.Lax;

            // cookie config
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie = cookieBuilder;
                config.LoginPath = "/User/SignIn";
                config.AccessDeniedPath = "/User/SignIn";
                config.ExpireTimeSpan = TimeSpan.FromHours(2);
                config.SlidingExpiration = true;
            });
            
        }

        /// <summary>
        /// automapper konfigurasyonu
        /// </summary>
        /// <param name="services"></param>
        public static void AddDtoMapConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                
            });
        }

        /// <summary>
        /// cors konfigurasyonu
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(config =>
            {
                config.AddPolicy(
                    name: "FullAllow",
                    builder => {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
        }
    }
}
