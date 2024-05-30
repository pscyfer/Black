using Microsoft.EntityFrameworkCore;
using TicketModule.Configuration;
using TicketModule.Entities;

namespace TicketModule
{
    public class TicketModuleContext : DbContext
    {
        public TicketModuleContext(DbContextOptions<TicketModuleContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketConfig).Assembly);
            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }
    }
}