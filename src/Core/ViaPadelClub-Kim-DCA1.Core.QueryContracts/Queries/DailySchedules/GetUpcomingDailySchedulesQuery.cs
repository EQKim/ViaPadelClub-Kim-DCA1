namespace ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.DailySchedules;

public sealed record GetUpcomingDailySchedulesQuery(int Count = 3);

public sealed record UpcomingDailySchedulesAnswer(
    IReadOnlyList<UpcomingDailyScheduleDto> DailySchedules);

public sealed record UpcomingDailyScheduleDto(
    Guid DailyScheduleId,
    string Status,
    DateTime WindowStart,
    DateTime WindowEnd,
    IReadOnlyList<UpcomingDailyScheduleCourtDto> Courts);

public sealed record UpcomingDailyScheduleCourtDto(
    Guid DailyScheduleCourtId,
    Guid CourtId,
    bool IsVipOnly,
    int ActiveBookings,
    IReadOnlyList<UpcomingBookingDto> Bookings);

public sealed record UpcomingBookingDto(
    Guid BookingId,
    Guid PlayerId,
    DateTime SlotStart,
    DateTime SlotEnd,
    string Status);
