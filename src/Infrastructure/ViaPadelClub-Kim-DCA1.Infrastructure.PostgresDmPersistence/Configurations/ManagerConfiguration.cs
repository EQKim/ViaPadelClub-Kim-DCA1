using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Managers;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Configurations;

public sealed class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.ToTable("managers");

        builder.HasKey(manager => manager.Id);

        builder.Property(manager => manager.Id)
            .HasColumnName("id")
            .HasConversion(id => id.Value, value => new ManagerId(value));

        builder.Property(manager => manager.PadelCompanyName)
            .HasColumnName("padel_company_name")
            .HasMaxLength(200);
    }
}
