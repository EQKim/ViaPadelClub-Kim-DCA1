using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.AddCourt;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.DailySchedules;

[ApiController]
[Route("api/daily-schedules")]
public sealed class AddCourtEndpoint
    : ApiEndpoint.WithRequest<AddCourtRequest>.WithResponse<AddCourtResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AddCourtEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{dailyScheduleId:guid}/courts")]
    public async Task<ActionResult<AddCourtResponse>> HandleAsync(
        Guid dailyScheduleId,
        [FromBody] AddCourtRequest request)
    {
        try
        {
            Result<AddCourtCommand> commandResult =
                AddCourtCommand.Create(
                    dailyScheduleId,
                    request.DailyScheduleCourtId,
                    request.CourtId,
                    request.IsVipOnly);

            if (commandResult.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));

            return Ok(new AddCourtResponse(request.DailyScheduleCourtId));
        }
        catch (Exception exception)
        {
            return Problem(exception.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [NonAction]
    public override Task<ActionResult<AddCourtResponse>> HandleAsync(AddCourtRequest request)
    {
        throw new NotSupportedException("Use the route overload with a daily schedule identifier.");
    }
}

public sealed record AddCourtRequest(Guid DailyScheduleCourtId, Guid CourtId, bool IsVipOnly);

public sealed record AddCourtResponse(Guid DailyScheduleCourtId);
