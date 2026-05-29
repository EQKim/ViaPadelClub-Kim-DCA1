using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.UnbanPlayer;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Players;

[ApiController]
[Route("api/players")]
public sealed class UnbanPlayerEndpoint
    : ApiEndpoint.WithoutRequest.WithResponse<UnbanPlayerResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public UnbanPlayerEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{playerId:guid}/unban")]
    public async Task<ActionResult<UnbanPlayerResponse>> HandleAsync(
        Guid playerId,
        [FromBody] PlayerAdminActionRequest request)
    {
        try
        {
            Result<UnbanPlayerCommand> commandResult =
                UnbanPlayerCommand.Create(playerId, request.ManagerId, request.Reason);

            if (commandResult.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));

            return Ok(new UnbanPlayerResponse(playerId));
        }
        catch (Exception exception)
        {
            return Problem(exception.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [NonAction]
    public override Task<ActionResult<UnbanPlayerResponse>> HandleAsync()
    {
        throw new NotSupportedException("Use the route overload with a player identifier.");
    }
}

public sealed record UnbanPlayerResponse(Guid PlayerId);
