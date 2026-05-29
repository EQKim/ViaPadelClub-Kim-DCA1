namespace ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.Players;

public sealed record GetPlayerBookingsQuery(Guid PlayerId, bool ActiveOnly = true);

public sealed record PlayerBookingsAnswer(IReadOnlyList<PlayerBookingDto> Bookings);

public sealed record PlayerBookingDto(
    Guid BookingId,
    Guid PlayerId,
    Guid DailyScheduleId,
    Guid DailyScheduleCourtId,
    Guid CourtId,
    string CourtName,
    DateTime SlotStart,
    DateTime SlotEnd,
    string Status,
    DateTime ScheduleWindowStart,
    DateTime ScheduleWindowEnd);
