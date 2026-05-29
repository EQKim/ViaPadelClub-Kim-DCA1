using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;

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
    public async Task<IReadOnlyList<DailySchedule>> GetSchedulesWithBookingsForPlayerAsync(PlayerId playerId, DateTime from)
    {
        return await Context.DailySchedules
            .Include(schedule => schedule.Courts)
            .ThenInclude(court => court.Bookings)
            .Where(schedule => schedule.Courts.Any(court =>
                court.Bookings.Any(booking =>
                    booking.PlayerId == playerId &&
                    booking.Status == "Active" &&
                    booking.Slot.Start >= from)))
            .ToListAsync();
    }
}
