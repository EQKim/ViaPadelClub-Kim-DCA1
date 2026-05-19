using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence;

public sealed class DmContext : DbContext
{
    public DmContext(DbContextOptions<DmContext> options) : base(options)
    {
    }

    public DbSet<Player> Players => Set<Player>();
    public DbSet<DailySchedule> DailySchedules => Set<DailySchedule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DmContext).Assembly);
    }
}
