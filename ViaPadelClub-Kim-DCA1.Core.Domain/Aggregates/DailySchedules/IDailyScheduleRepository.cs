using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;

public interface IDailyScheduleRepository : IRepository<DailySchedule, DailyScheduleId>
{
    Task<IReadOnlyList<DailySchedule>> GetSchedulesWithBookingsForPlayerAsync(PlayerId playerId, DateTime from);
}
