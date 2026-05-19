using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Repositories;

public sealed class DailyScheduleRepository : Repository<DailySchedule, DailyScheduleId>, IDailyScheduleRepository
{
    public DailyScheduleRepository(DmContext context) : base(context)
    {
    }

    public override Task<DailySchedule?> GetByIdAsync(DailyScheduleId id)
    {
        return Context.DailySchedules
            .Include(schedule => schedule.Courts)
            .ThenInclude(court => court.Bookings)
            .FirstOrDefaultAsync(schedule => schedule.Id == id);
    }
}
