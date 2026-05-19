using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Configurations;

public sealed class DailyScheduleCourtConfiguration : IEntityTypeConfiguration<DailyScheduleCourt>
{
    public void Configure(EntityTypeBuilder<DailyScheduleCourt> builder)
    {
        builder.ToTable("daily_schedule_courts");

        builder.HasKey(court => court.Id);

        builder.Property(court => court.Id)
            .HasColumnName("id")
            .HasConversion(id => id.Value, value => new DailyScheduleCourtId(value));

        builder.Property(court => court.CourtId)
            .HasColumnName("court_id")
            .HasConversion(id => id.Value, value => new CourtId(value));

        builder.Property(court => court.IsVipOnly)
            .HasColumnName("is_vip_only");

        builder.HasMany(court => court.Bookings)
            .WithOne()
            .HasForeignKey("daily_schedule_court_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(court => court.Bookings)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
