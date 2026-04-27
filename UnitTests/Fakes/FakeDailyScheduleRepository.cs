using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;

namespace UnitTests.Fakes;

public sealed class FakeDailyScheduleRepository : IDailyScheduleRepository
{
    private readonly List<DailySchedule> _dailySchedules = new();

    public IReadOnlyList<DailySchedule> DailySchedules => _dailySchedules;

    public Task AddAsync(DailySchedule dailySchedule)
    {
        _dailySchedules.Add(dailySchedule);
        return Task.CompletedTask;
    }

    public Task<DailySchedule?> GetByIdAsync(DailyScheduleId id)
    {
        DailySchedule? dailySchedule = _dailySchedules.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(dailySchedule);
    }
}