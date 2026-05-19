using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Tools.ObjectMapper;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.DailySchedules;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Mappings;

public sealed class GetUpcomingDailySchedulesRequestMapping
    : IMapping<GetUpcomingDailySchedulesRequest, GetUpcomingDailySchedulesQuery>
{
    public GetUpcomingDailySchedulesQuery Map(GetUpcomingDailySchedulesRequest source)
    {
        return new GetUpcomingDailySchedulesQuery(source.Count);
    }
}

public sealed class UpcomingDailySchedulesAnswerMapping
    : IMapping<UpcomingDailySchedulesAnswer, GetUpcomingDailySchedulesResponse>
{
    public GetUpcomingDailySchedulesResponse Map(UpcomingDailySchedulesAnswer source)
    {
        return new GetUpcomingDailySchedulesResponse(source.DailySchedules
            .Select(schedule => new UpcomingDailyScheduleResponseItem(
                schedule.DailyScheduleId,
                schedule.Status,
                schedule.WindowStart,
                schedule.WindowEnd,
                schedule.Courts
                    .Select(court => new UpcomingDailyScheduleCourtResponseItem(
                        court.DailyScheduleCourtId,
                        court.CourtId,
                        court.IsVipOnly,
                        court.ActiveBookings,
                        court.Bookings
                            .Select(booking => new UpcomingBookingResponseItem(
                                booking.BookingId,
                                booking.PlayerId,
                                booking.SlotStart,
                                booking.SlotEnd,
                                booking.Status))
                            .ToList()))
                    .ToList()))
            .ToList());
    }
}
