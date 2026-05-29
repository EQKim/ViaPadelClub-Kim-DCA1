using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.Players;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Handlers;

public sealed class GetPlayerBookingsQueryHandler
    : IQueryHandler<GetPlayerBookingsQuery, PlayerBookingsAnswer>
{
    private readonly QueryDbContext _context;

    public GetPlayerBookingsQueryHandler(QueryDbContext context)
    {
        _context = context;
    }

    public async Task<PlayerBookingsAnswer> HandleAsync(GetPlayerBookingsQuery query)
    {
        IQueryable<Models.BookingReadModel> bookings = _context.Bookings
            .AsNoTracking()
            .Where(booking => booking.PlayerId == query.PlayerId);

        if (query.ActiveOnly)
        {
            bookings = bookings.Where(booking => booking.Status == "Active");
        }

        List<PlayerBookingDto> result = await bookings
            .OrderBy(booking => booking.SlotStart)
            .ThenBy(booking => booking.Id)
            .Select(booking => new PlayerBookingDto(
                booking.Id,
                booking.PlayerId,
                booking.DailyScheduleCourt.DailyScheduleId,
                booking.DailyScheduleCourtId,
                booking.DailyScheduleCourt.CourtId,
                _context.Courts
                    .Where(court => court.Id == booking.DailyScheduleCourt.CourtId)
                    .Select(court => court.CourtName)
                    .FirstOrDefault() ?? string.Empty,
                booking.SlotStart,
                booking.SlotEnd,
                booking.Status,
                booking.DailyScheduleCourt.DailySchedule.WindowStart,
                booking.DailyScheduleCourt.DailySchedule.WindowEnd))
            .ToListAsync();

        return new PlayerBookingsAnswer(result);
    }
}
