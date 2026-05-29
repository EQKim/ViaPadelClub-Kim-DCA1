using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Managers;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Repositories;

public sealed class ManagerRepository : Repository<Manager, ManagerId>, IManagerRepository
{
    public ManagerRepository(DmContext context) : base(context)
    {
    }
}
