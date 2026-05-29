using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Configurations;

public sealed class AdminPlayerActionConfiguration : IEntityTypeConfiguration<AdminPlayerAction>
{
    public void Configure(EntityTypeBuilder<AdminPlayerAction> builder)
    {
        builder.ToTable("admin_player_actions");

        builder.HasKey(action => action.Id);

        builder.Property(action => action.Id)
            .HasColumnName("id");

        builder.Property(action => action.ActionType)
            .HasColumnName("action_type")
            .HasMaxLength(40);

        builder.Property(action => action.ManagerId)
            .HasColumnName("manager_id")
            .HasConversion(id => id.Value, value => new ManagerId(value));

        builder.Property(action => action.Reason)
            .HasColumnName("reason")
            .HasMaxLength(500);

        builder.Property(action => action.Timestamp)
            .HasColumnName("timestamp")
            .HasColumnType("timestamp without time zone");
    }
}
