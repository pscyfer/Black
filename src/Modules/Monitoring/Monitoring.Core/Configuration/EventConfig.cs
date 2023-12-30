using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Core.Entities;

namespace Monitoring.Core.Configuration;

public class EventConfig : DateTimeConverter, IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DateTime)
            .IsRequired(true)
              .HasColumnType("bigint")
            .HasConversion(
            v => ToMilliseconds(v),       // Convert DateTime to long
            v => ToDateTime(v) // Convert long to DateTime
        );
      
    }
}