using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Monitoring.Core.Configuration
{
    public class MonitorConfig : DateTimeConverter, IEntityTypeConfiguration<Entities.Monitor>
    {
        public void Configure(EntityTypeBuilder<Entities.Monitor> builder)
        {

            builder.ToTable("Monitors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LastChecked)
                   .HasColumnType("bigint")
                 .HasConversion(
            v => ToMilliseconds(v),       // Convert DateTime to long
            v => ToDateTime(v));

            builder.Property(x => x.UpTimeFor)
                .HasColumnType("bigint")
                .HasConversion(
            v => ToMilliseconds(v),       // Convert DateTime to long
            v => ToDateTime(v));

            builder.OwnsOne(x => x.Http, config =>
            {
                config.Property(x => x.DomainExpierDate)
                 .HasColumnType("bigint")
                  .HasConversion(
            v => ToMilliseconds(v),       // Convert DateTime to long
            v => ToDateTime(v));


            });

            builder.Property(x => x.Ip).IsRequired(true).HasMaxLength(100);


            builder.HasMany(x => x.Incidents)
                .WithOne(x => x.Monitor)
                .HasForeignKey(x => x.MonitorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Events)
                .WithOne(x => x.Monitor)
                .HasForeignKey(x => x.MonitorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ResponsTimes)
                .WithOne(x => x.Monitor)
                .HasForeignKey(x => x.MonitorId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
