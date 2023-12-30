using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketModule.Entities;

namespace TicketModule.Configuration
{
    internal class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets", "Support");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Messages).WithOne(x => x.Ticket)
                 .HasForeignKey(x => x.TicketId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
