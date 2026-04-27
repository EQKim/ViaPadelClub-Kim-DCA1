using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;

public interface IDailyScheduleRepository
{
    Task AddAsync(DailySchedule dailySchedule);
    Task<DailySchedule?> GetByIdAsync(DailyScheduleId id);
}