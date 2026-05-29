using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Configurations;

public sealed class CourtConfiguration : IEntityTypeConfiguration<Court>
{
    public void Configure(EntityTypeBuilder<Court> builder)
    {
        builder.ToTable("courts");

        builder.HasKey(court => court.Id);

        builder.Property(court => court.Id)
            .HasColumnName("id")
            .HasConversion(id => id.Value, value => new CourtId(value));

        builder.Property(court => court.CourtName)
            .HasColumnName("court_name")
            .HasMaxLength(200);
    }
}
