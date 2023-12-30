using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Core.Entities;

namespace Monitoring.Core.Configuration;

public class ResponsTimeConfig : DateTimeConverter, IEntityTypeConfiguration<ResponsTimeMonitor>
{
    public void Configure(EntityTypeBuilder<ResponsTimeMonitor> builder)
    {
        builder.ToTable("MonitorResponsTime");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DateTime)
              .HasConversion(
            v => ToMilliseconds(v),       // Convert DateTime to long
            v => ToDateTime(v));

        builder.Property(x => x.ResponsTime)
            .IsRequired(true);
    }
}