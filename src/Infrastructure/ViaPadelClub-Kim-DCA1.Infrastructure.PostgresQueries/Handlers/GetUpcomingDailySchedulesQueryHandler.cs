using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Time;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Handlers;

public sealed class GetUpcomingDailySchedulesQueryHandler
    : IQueryHandler<GetUpcomingDailySchedulesQuery, UpcomingDailySchedulesAnswer>
{
    private readonly QueryDbContext _context;
    private readonly ISystemTime _systemTime;

    public GetUpcomingDailySchedulesQueryHandler(
        QueryDbContext context,
        ISystemTime systemTime)
    {
        _context = context;
        _systemTime = systemTime;
    }

    public async Task<UpcomingDailySchedulesAnswer> HandleAsync(GetUpcomingDailySchedulesQuery query)
    {
        int count = query.Count <= 0 ? 3 : query.Count;
        DateTime currentTime = _systemTime.CurrentTime();

        List<UpcomingDailyScheduleDto> schedules = await _context.DailySchedules
            .AsNoTracking()
            .Where(schedule => schedule.WindowEnd >= currentTime)
            .OrderBy(schedule => schedule.WindowStart)
            .Take(count)
            .Select(schedule => new UpcomingDailyScheduleDto(
                schedule.Id,
                schedule.Status,
                schedule.WindowStart,
                schedule.WindowEnd,
                schedule.Courts
                    .OrderBy(court => court.CourtId)
                    .Select(court => new UpcomingDailyScheduleCourtDto(
                        court.Id,
                        court.CourtId,
                        _context.Courts
                            .Where(courtReadModel => courtReadModel.Id == court.CourtId)
                            .Select(courtReadModel => courtReadModel.CourtName)
                            .FirstOrDefault() ?? string.Empty,
                        court.IsVipOnly,
                        court.Bookings.Count(booking => booking.Status == "Active"),
                        court.Bookings
                            .OrderBy(booking => booking.SlotStart)
                            .Select(booking => new UpcomingBookingDto(
                                booking.Id,
                                booking.PlayerId,
                                booking.SlotStart,
                                booking.SlotEnd,
                                booking.Status))
                            .ToList()))
                    .ToList()))
            .ToListAsync();

        return new UpcomingDailySchedulesAnswer(schedules);
    }
}
