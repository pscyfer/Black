using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketModule.Entities;

namespace TicketModule.Configuration
{
    internal class TicketMessageConfig : IEntityTypeConfiguration<TicketMessage>
    {
        public void Configure(EntityTypeBuilder<TicketMessage> builder)
        {
            builder.ToTable("TicketMessages", "Support");
            builder.HasKey(x => x.Id);
        }
    }
}
