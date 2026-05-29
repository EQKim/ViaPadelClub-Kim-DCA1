using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CancelBooking;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.DailySchedules;

[ApiController]
[Route("api/daily-schedules")]
public sealed class CancelBookingEndpoint
    : ApiEndpoint.WithoutRequest.WithResponse<CancelBookingResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public CancelBookingEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{dailyScheduleId:guid}/courts/{dailyScheduleCourtId:guid}/bookings/{bookingId:guid}/cancel")]
    public async Task<ActionResult<CancelBookingResponse>> HandleAsync(
        Guid dailyScheduleId,
        Guid dailyScheduleCourtId,
        Guid bookingId)
    {
        try
        {
            Result<CancelBookingCommand> commandResult =
                CancelBookingCommand.Create(dailyScheduleId, dailyScheduleCourtId, bookingId);

            if (commandResult.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));

            return Ok(new CancelBookingResponse(bookingId));
        }
        catch (Exception exception)
        {
            return Problem(ExceptionResponseFactory.GetDetail(exception), statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [NonAction]
    public override Task<ActionResult<CancelBookingResponse>> HandleAsync()
    {
        throw new NotSupportedException("Use the route overload with daily schedule, court, and booking identifiers.");
    }
}

public sealed record CancelBookingResponse(Guid BookingId);
