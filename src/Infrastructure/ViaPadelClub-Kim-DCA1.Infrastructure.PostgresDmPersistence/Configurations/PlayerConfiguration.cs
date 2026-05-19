using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Configurations;

public sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players");

        builder.HasKey(player => player.Id);

        builder.Property(player => player.Id)
            .HasColumnName("id")
            .HasConversion(id => id.Value, value => new PlayerId(value));

        builder.Property(player => player.UniversityName)
            .HasColumnName("university_name")
            .HasMaxLength(200)
            .HasConversion(name => name.Value, value => new UniversityName(value));

        builder.Property(player => player.IsVip)
            .HasColumnName("is_vip");

        builder.Property(player => player.IsBanned)
            .HasColumnName("is_banned");
    }
}
