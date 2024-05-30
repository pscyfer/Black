using Certificate.Checker;
using Certificate.Checker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Monitoring.Abstractions.Interfaces;
using Monitoring.Core;
using Monitoring.DomainExpirationChecker;
using Monitoring.DomainExpirationChecker.Interface;
using Monitoring.EF_Services;
using Monitoring.UpTimeServices;

namespace Monitoring;

public static class MonitorBootsrapper
{
    public static void ConfigureMonitor(this IServiceCollection service, string? connectionString)
    {
        service.AddDbContext<MonitorDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString, x =>
           x.MigrationsHistoryTable("_Monitor_MigrationsHistory", "Monitor"));
        });
        service.AddTransient<IHttpRequestToolsService, HttpRequestToolsService>();

        service.AddScoped<IHttpRequestMonitoringService, HttpRequestMonitoringService>();
        service.AddScoped<IEventMonitoringService, EventMonitoringService>();
        service.AddScoped<IUpTimeService, UpTimeService>();
        service.AddScoped<IResponsTimeService, ResponsTimeService>();

        service.AddMemoryCache();

        service.AddCertificateChecker();
        service.AddDomainExpirationChecker();
    }
    public static IServiceCollection AddCertificateChecker(this IServiceCollection services)
    {

        services.AddSingleton<ICertificateStore, CertificateStore>();
        services.AddSingleton<CertificateExtractionHandler>();
        //services.AddHttpClient<ICertificateCheckerService, CertificateCheckerService>()
        //                .ConfigurePrimaryHttpMessageHandler((serviceProvider) => 
        //                serviceProvider.GetRequiredService<CertificateExtractionHandler>());
        return services;
    }
    public static IServiceCollection AddDomainExpirationChecker(this IServiceCollection services)
    {
        services.AddSingleton<IDomainExpiertionStore, DomainExpiertionStore>();
        services.AddSingleton<IDomainExpirationCheckerService, DomainExpirationCheckerService>();
        return services;
    }
}