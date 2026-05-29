using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.ActivateDailySchedule;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.DailySchedules;

[ApiController]
[Route("api/daily-schedules")]
public sealed class ActivateDailyScheduleEndpoint
    : ApiEndpoint.WithoutRequest.WithResponse<ActivateDailyScheduleResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public ActivateDailyScheduleEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{dailyScheduleId:guid}/activate")]
    public async Task<ActionResult<ActivateDailyScheduleResponse>> HandleAsync(Guid dailyScheduleId)
    {
        try
        {
            Result<ActivateDailyScheduleCommand> commandResult =
                ActivateDailyScheduleCommand.Create(dailyScheduleId);

            if (commandResult.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));

            return Ok(new ActivateDailyScheduleResponse(dailyScheduleId));
        }
        catch (Exception exception)
        {
            return Problem(ExceptionResponseFactory.GetDetail(exception), statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [NonAction]
    public override Task<ActionResult<ActivateDailyScheduleResponse>> HandleAsync()
    {
        throw new NotSupportedException("Use the route overload with a daily schedule identifier.");
    }
}

public sealed record ActivateDailyScheduleResponse(Guid DailyScheduleId);
