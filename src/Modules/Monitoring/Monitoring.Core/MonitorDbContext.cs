using Microsoft.EntityFrameworkCore;
using Monitoring.Core.Configuration;
using Monitoring.Core.Entities;

namespace Monitoring.Core;

public class MonitorDbContext : DbContext
{
    public MonitorDbContext(DbContextOptions<MonitorDbContext> options)
    : base(options)
    {

    }



    public DbSet<Entities.Monitor> Monitors { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<ResponsTimeMonitor> ResponsOfRequests { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeConverter>();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Monitor");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MonitorConfig).Assembly);
    }


}