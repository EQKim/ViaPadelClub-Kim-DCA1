using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.BanPlayer;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Players;

[ApiController]
[Route("api/players")]
public sealed class BanPlayerEndpoint
    : ApiEndpoint.WithoutRequest.WithResponse<BanPlayerResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public BanPlayerEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{playerId:guid}/ban")]
    public async Task<ActionResult<BanPlayerResponse>> HandleAsync(
        Guid playerId,
        [FromBody] PlayerAdminActionRequest request)
    {
        try
        {
            Result<BanPlayerCommand> commandResult =
                BanPlayerCommand.Create(playerId, request.ManagerId, request.Reason);

            if (commandResult.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));

            return Ok(new BanPlayerResponse(playerId));
        }
        catch (Exception exception)
        {
            return Problem(ExceptionResponseFactory.GetDetail(exception), statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [NonAction]
    public override Task<ActionResult<BanPlayerResponse>> HandleAsync()
    {
        throw new NotSupportedException("Use the route overload with a player identifier.");
    }
}

public sealed record BanPlayerResponse(Guid PlayerId);

public sealed record PlayerAdminActionRequest(Guid ManagerId, string Reason);
