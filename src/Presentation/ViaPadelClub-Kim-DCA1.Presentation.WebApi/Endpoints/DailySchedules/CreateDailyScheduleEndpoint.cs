using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateDailySchedule;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.DailySchedules;

[ApiController]
[Route("api/daily-schedules")]
public sealed class CreateDailyScheduleEndpoint
    : ApiEndpoint.WithRequest<CreateDailyScheduleRequest>.WithResponse<CreateDailyScheduleResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public CreateDailyScheduleEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public override async Task<ActionResult<CreateDailyScheduleResponse>> HandleAsync(
        [FromBody] CreateDailyScheduleRequest request)
    {
        try
        {
            Result<CreateDailyScheduleCommand> commandResult =
                CreateDailyScheduleCommand.Create(
                    request.DailyScheduleId,
                    request.ManagerId,
                    request.WindowStart,
                    request.WindowEnd);

            if (commandResult.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));

            return Ok(new CreateDailyScheduleResponse(request.DailyScheduleId));
        }
        catch (Exception exception)
        {
            return Problem(exception.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}

public sealed record CreateDailyScheduleRequest(
    Guid DailyScheduleId,
    Guid ManagerId,
    DateTime WindowStart,
    DateTime WindowEnd);

public sealed record CreateDailyScheduleResponse(Guid DailyScheduleId);
