using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpinningFilm.Helpers;
using SpinningFilm.Services;
using SpinningFilm.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SpinningFilm.Models;
using SpinningFilm.Infrastructure;
using SpinningFilm.Interfaces;
using SpinningFilm.Auth;
using Microsoft.AspNetCore.Authorization;

namespace SpinningFilm
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
            var appSettingsSections = Configuration.GetSection("AppSettings");
            services.Configure<ApiSettings>(appSettingsSections);
            
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddDbContext<SpinningFilmContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SpinningFilmConnection")));

            services.AddIdentityCore<AppUser>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    // Cookie settings
                    options.Cookie.Name = "spinningfilm";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.SlidingExpiration = true;
                });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TestUser", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new SameUserRequirement());
                });
            });

            services.AddSingleton<IAuthorizationHandler, MediaAuthorizationHandler>();

            services.AddScoped(typeof(IMediaRepository<>), typeof(MediaRepository<>));
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IUserStore<AppUser>, MediaUserStore>();
            services.AddScoped<IMediaListViewModelService, MediaListViewModelService>();
            services.AddScoped<IMediaInformationViewModelService, MediaInformationViewModelService>();
            services.AddScoped<IWatchedViewModelService, WatchedViewModelService>();
            services.AddScoped<IPhysicalMediaService, PhysicalMediaService>();
            services.AddScoped<IEditMediaViewModelService, EditMediaViewModelService>();
            
            //services.ConfigureExternalCookie(options => 
            //{
            //    options.Cookie.Name = "test.test";
            //});
            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.Name = "spinningfilm";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            //    options.LoginPath = "/Account/Login";
            //    options.AccessDeniedPath = "/Account/AccessDenied";
            //    options.SlidingExpiration = true;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithReExecute("/home/error/{0}");
            }
            else
            {
                app.UseStatusCodePages();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
