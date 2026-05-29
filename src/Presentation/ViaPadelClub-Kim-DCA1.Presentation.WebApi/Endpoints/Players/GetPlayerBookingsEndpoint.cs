using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.Players;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Players;

[ApiController]
[Route("api/players")]
public sealed class GetPlayerBookingsEndpoint
    : ApiEndpoint.WithRequest<GetPlayerBookingsRequest>.WithResponse<GetPlayerBookingsResponse>
{
    private readonly IQueryDispatcher _queryDispatcher;

    public GetPlayerBookingsEndpoint(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{playerId:guid}/bookings")]
    public async Task<ActionResult<GetPlayerBookingsResponse>> HandleAsync(
        Guid playerId,
        [FromQuery] GetPlayerBookingsRequest request)
    {
        try
        {
            PlayerBookingsAnswer answer =
                await _queryDispatcher.DispatchAsync<GetPlayerBookingsQuery, PlayerBookingsAnswer>(
                    new GetPlayerBookingsQuery(playerId, request.ActiveOnly));

            GetPlayerBookingsResponse response = new(answer.Bookings
                .Select(booking => new PlayerBookingResponseItem(
                    booking.BookingId,
                    booking.PlayerId,
                    booking.DailyScheduleId,
                    booking.DailyScheduleCourtId,
                    booking.CourtId,
                    booking.CourtName,
                    booking.SlotStart,
                    booking.SlotEnd,
                    booking.Status,
                    booking.ScheduleWindowStart,
                    booking.ScheduleWindowEnd))
                .ToList());

            return Ok(response);
        }
        catch (Exception exception)
        {
            return Problem(ExceptionResponseFactory.GetDetail(exception), statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [NonAction]
    public override Task<ActionResult<GetPlayerBookingsResponse>> HandleAsync(GetPlayerBookingsRequest request)
    {
        throw new NotSupportedException("Use the route overload with a player identifier.");
    }
}

public sealed record GetPlayerBookingsRequest(bool ActiveOnly = true);

public sealed record GetPlayerBookingsResponse(IReadOnlyList<PlayerBookingResponseItem> Bookings);

public sealed record PlayerBookingResponseItem(
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
