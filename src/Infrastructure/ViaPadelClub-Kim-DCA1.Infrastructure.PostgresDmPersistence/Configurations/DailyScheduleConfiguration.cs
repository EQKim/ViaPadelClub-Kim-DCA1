using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Configurations;

public sealed class DailyScheduleConfiguration : IEntityTypeConfiguration<DailySchedule>
{
    public void Configure(EntityTypeBuilder<DailySchedule> builder)
    {
        builder.ToTable("daily_schedules");

        builder.HasKey(schedule => schedule.Id);

        builder.Property(schedule => schedule.Id)
            .HasColumnName("id")
            .HasConversion(id => id.Value, value => new DailyScheduleId(value));

        builder.Property(schedule => schedule.ManagerId)
            .HasColumnName("manager_id")
            .HasConversion(id => id.Value, value => new ManagerId(value));

        builder.OwnsOne(schedule => schedule.Window, window =>
        {
            window.Property(value => value.Start)
                .HasColumnName("window_start")
                .HasColumnType("timestamp without time zone");

            window.Property(value => value.End)
                .HasColumnName("window_end")
                .HasColumnType("timestamp without time zone");
        });

        builder.Property(schedule => schedule.Status)
            .HasColumnName("status")
            .HasMaxLength(40);

        builder.HasMany(schedule => schedule.Courts)
            .WithOne()
            .HasForeignKey("daily_schedule_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(schedule => schedule.Courts)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
