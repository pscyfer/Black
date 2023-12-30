using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TicketModule.AutoMapperProfiles;
using TicketModule.Services;

namespace TicketModule
{
    public static class TicketManagementBootstrapper
    {
        public static IServiceCollection ConfigureSupportTicket(this IServiceCollection services, string? connectionString)
        {

            if (connectionString is null) throw new ArgumentNullException(nameof(connectionString));
            //addServices
            services.AddDbContext<TicketModuleContext>(opt =>
            {
                opt.UseSqlServer(connectionString,
                    x =>
                 x.MigrationsHistoryTable("_Support_MigrationsHistory", "Support"));



            });

            services.AddScoped<ITicketService, TicketService>();

            //AddAutoMapper
            services.AddAutoMapper(typeof(TicketProfile).Assembly);
            return services;
        }
    }
}
