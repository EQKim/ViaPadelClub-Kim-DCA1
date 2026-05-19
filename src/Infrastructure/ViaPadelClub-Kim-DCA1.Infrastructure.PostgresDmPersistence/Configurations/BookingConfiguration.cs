using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Configurations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");

        builder.HasKey(booking => booking.Id);

        builder.Property(booking => booking.Id)
            .HasColumnName("id")
            .HasConversion(id => id.Value, value => new BookingId(value));

        builder.Property(booking => booking.PlayerId)
            .HasColumnName("player_id")
            .HasConversion(id => id.Value, value => new PlayerId(value));

        builder.OwnsOne(booking => booking.Slot, slot =>
        {
            slot.Property(value => value.Start)
                .HasColumnName("slot_start")
                .HasColumnType("timestamp without time zone");

            slot.Property(value => value.End)
                .HasColumnName("slot_end")
                .HasColumnType("timestamp without time zone");
        });

        builder.Property(booking => booking.Status)
            .HasColumnName("status")
            .HasMaxLength(40);
    }
}
