using Common.AspNetCore.Autorizetion.DynamicAuthorizationService;
using Common.AspNetCore.Autorizetion.DynamicAuthorizationService.Enum;
using Identity.Core.Repository.Roles;
using Identity.Core.Repository.Users;
using Identity.Core.Security;
using Identity.Core.Services;
using Identity.Data;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Identity.Core
{
    public static class IdentityBootstrapper
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, string connectionString)
        {
            //AddServices
            services.AddTransient<IUnitOfWork, IdentityContext>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<PersianIdentityErrorDescriber>();
            services.AddTransient<IUserService, UserServices>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
            //services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IIdentityDbInitializer, IdentityDbInitializer>();
            //addDbContextServices
            services.AddDbContext<IdentityContext>(opt =>
            {
                opt.UseSqlServer(connectionString, x =>
               x.MigrationsHistoryTable("_Identity_MigrationsHistory", "Identity"));
            });
           

            //ConfigIdentity
            services.AddIdentity<User, Role>(
                  options =>
                  {
                      //Configure Password
                      // options.Password.RequireDigit = false;
                      // options.Password.RequiredLength = 6;
                      // options.Password.RequiredUniqueChars = 1;
                      // options.Password.RequireLowercase = false;
                      // options.Password.RequireNonAlphanumeric = false;
                      // options.Password.RequireUppercase = false;

                      options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                      options.User.RequireUniqueEmail = false;
                      options.SignIn.RequireConfirmedEmail = false;
                      options.SignIn.RequireConfirmedPhoneNumber = false;
                      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                      options.Lockout.MaxFailedAccessAttempts = 5;
                      options.Lockout.AllowedForNewUsers = false;
                  })
              .AddErrorDescriber<PersianIdentityErrorDescriber>()
              .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
                    {

                        options.Cookie.HttpOnly = true;
                        options.ExpireTimeSpan = TimeSpan.FromDays(30);
                        options.Cookie.Name = "AuthApplicationCookie";
                        options.LoginPath = "/Authentication/Login";
                        options.AccessDeniedPath = "/AccessDenide";

                    });

            services.AddDynamicPermissionService();

            //AddAutoMapper
            // services.AddAutoMapper(typeof(IdentityBootstrapper).Assembly);
            return services;
        }

        public static IServiceCollection AddDynamicPermissionService(this IServiceCollection service)
        {
            service.AddSingleton<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
            service.AddSingleton<IMvcActionsDiscoveryService, MvcActionsDiscoveryService>();
            service.AddSingleton<ISecurityTrimmingService, SecurityTrimmingService>();
            return service;
        }
        public static void UseCustomIdentityServices(this IApplicationBuilder app)
        {
            app.CallDbInitializer();
        }
        private static void CallDbInitializer(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var identityDbInitialize = scope.ServiceProvider.GetService<IIdentityDbInitializer>();
            identityDbInitialize?.Initialize();
            identityDbInitialize?.SeedData();
        }
    }

}
