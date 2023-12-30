using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Core.Entities;

namespace Monitoring.Core.Configuration;

public class IncidentConfig : DateTimeConverter, IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("Incidents");
        builder.Property(x => x.StartAt)
            .IsRequired()
              .HasColumnType("bigint")
            .HasConversion(
            v => ToMilliseconds(v),       
            v => ToDateTime(v));
    }
}