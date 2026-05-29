using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Tools.ObjectMapper;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.DailySchedules;

[ApiController]
[Route("api/daily-schedules")]
public sealed class GetUpcomingDailySchedulesEndpoint
    : ApiEndpoint.WithRequest<GetUpcomingDailySchedulesRequest>.WithResponse<GetUpcomingDailySchedulesResponse>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IObjectMapper _objectMapper;

    public GetUpcomingDailySchedulesEndpoint(
        IQueryDispatcher queryDispatcher,
        IObjectMapper objectMapper)
    {
        _queryDispatcher = queryDispatcher;
        _objectMapper = objectMapper;
    }

    [HttpGet("upcoming")]
    public override async Task<ActionResult<GetUpcomingDailySchedulesResponse>> HandleAsync(
        [FromQuery] GetUpcomingDailySchedulesRequest request)
    {
        try
        {
            GetUpcomingDailySchedulesQuery query = _objectMapper.Map<GetUpcomingDailySchedulesQuery>(request);
            UpcomingDailySchedulesAnswer answer =
                await _queryDispatcher.DispatchAsync<GetUpcomingDailySchedulesQuery, UpcomingDailySchedulesAnswer>(query);

            return Ok(_objectMapper.Map<GetUpcomingDailySchedulesResponse>(answer));
        }
        catch (Exception exception)
        {
            return Problem(ExceptionResponseFactory.GetDetail(exception), statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}

public sealed record GetUpcomingDailySchedulesRequest(int Count = 3);

public sealed record GetUpcomingDailySchedulesResponse(
    IReadOnlyList<UpcomingDailyScheduleResponseItem> DailySchedules);

public sealed record UpcomingDailyScheduleResponseItem(
    Guid DailyScheduleId,
    string Status,
    DateTime WindowStart,
    DateTime WindowEnd,
    IReadOnlyList<UpcomingDailyScheduleCourtResponseItem> Courts);

public sealed record UpcomingDailyScheduleCourtResponseItem(
    Guid DailyScheduleCourtId,
    Guid CourtId,
    string CourtName,
    bool IsVipOnly,
    int ActiveBookings,
    IReadOnlyList<UpcomingBookingResponseItem> Bookings);

public sealed record UpcomingBookingResponseItem(
    Guid BookingId,
    Guid PlayerId,
    DateTime SlotStart,
    DateTime SlotEnd,
    string Status);
