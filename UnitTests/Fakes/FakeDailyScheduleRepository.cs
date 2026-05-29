using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;

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
    public Task<IReadOnlyList<DailySchedule>> GetSchedulesWithBookingsForPlayerAsync(PlayerId playerId, DateTime from)
    {
        IReadOnlyList<DailySchedule> dailySchedules = _dailySchedules
            .Where(schedule => schedule.Courts.Any(court =>
                court.Bookings.Any(booking =>
                    booking.PlayerId == playerId &&
                    booking.Status == "Active" &&
                    booking.Slot.Start >= from)))
            .ToList();

        return Task.FromResult(dailySchedules);
    }
}
