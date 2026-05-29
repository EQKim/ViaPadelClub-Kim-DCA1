using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateBooking;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.DailySchedules;

[ApiController]
[Route("api/daily-schedules")]
public sealed class CreateBookingEndpoint
    : ApiEndpoint.WithRequest<CreateBookingRequest>.WithResponse<CreateBookingResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public CreateBookingEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{dailyScheduleId:guid}/courts/{dailyScheduleCourtId:guid}/bookings")]
    public async Task<ActionResult<CreateBookingResponse>> HandleAsync(
        Guid dailyScheduleId,
        Guid dailyScheduleCourtId,
        [FromBody] CreateBookingRequest request)
    {
        try
        {
            Result<CreateBookingCommand> commandResult =
                CreateBookingCommand.Create(
                    dailyScheduleId,
                    dailyScheduleCourtId,
                    request.BookingId,
                    request.PlayerId,
                    request.SlotStart,
                    request.SlotEnd);

            if (commandResult.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));

            return Ok(new CreateBookingResponse(request.BookingId));
        }
        catch (Exception exception)
        {
            return Problem(exception.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [NonAction]
    public override Task<ActionResult<CreateBookingResponse>> HandleAsync(CreateBookingRequest request)
    {
        throw new NotSupportedException("Use the route overload with daily schedule and court identifiers.");
    }
}

public sealed record CreateBookingRequest(
    Guid BookingId,
    Guid PlayerId,
    DateTime SlotStart,
    DateTime SlotEnd);

public sealed record CreateBookingResponse(Guid BookingId);
