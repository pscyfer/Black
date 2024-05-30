using Certificate.Checker.Interfaces;
using Certificate.Checker;
using Common.AspNetCore.Autorizetion;
using Common.AspNetCore.Autorizetion.DynamicAuthorizationService;
using Common.AspNetCore.Autorizetion.DynamicAuthorizationService.Enum;
using Common.AspNetCore.LocalFileProvider;
using Common.AspNetCore.Notification;
using Identity.Core;
using Identity.Core.Security;
using JobsModule.Core;
using Monitoring;
using TicketModule;
using Common.AspNetCore.RazorService;

namespace Administrator.Infrastructure
{
    public static class RegisterDependencyServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.ConfigureIdentity(configuration.GetConnectionString("Default_Connection"));
            services.AddTransient<IRazorViewService, RazorViewService>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddAutoMapper(typeof(IdentityBootstrapper).Assembly);
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstantPolicies.RequireAdministratorRole, policy =>
                    policy.RequireRole(StandardRoles.SystemRoles));

                options.AddPolicy(ConstantPolicies.DynamicPermission, policy => policy.Requirements.Add(new DynamicPermissionRequirement()));
            });
            services.AddScoped<INotification, SweetAlertNotifcation>();

            #region Modules
            services.ConfigureJob(configuration.GetConnectionString("Hangfire_Connection"));
            #region Monitors
            services.ConfigureMonitor(configuration.GetConnectionString("Default_Connection"));

            services.AddHttpClient<ICertificateCheckerService, CertificateCheckerService>()
                      .ConfigurePrimaryHttpMessageHandler((serviceProvider) =>
                      serviceProvider.GetRequiredService<CertificateExtractionHandler>());
            #endregion
            services.ConfigureSupportTicket(configuration.GetConnectionString("Default_Connection"));
            #endregion

            services.Common_AspnetCoreServiceRegister();
        }

        public static void Common_AspnetCoreServiceRegister(this IServiceCollection services)
        {
            services.AddScoped<ITempDataService, TempDataService>();
        }
    }
}
