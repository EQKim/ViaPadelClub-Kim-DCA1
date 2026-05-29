using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RevokeVip;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Players;

[ApiController]
[Route("api/players")]
public sealed class RevokeVipEndpoint
    : ApiEndpoint.WithoutRequest.WithResponse<RevokeVipResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RevokeVipEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{playerId:guid}/revoke-vip")]
    public async Task<ActionResult<RevokeVipResponse>> HandleAsync(
        Guid playerId,
        [FromBody] PlayerAdminActionRequest request)
    {
        try
        {
            Result<RevokeVipCommand> commandResult =
                RevokeVipCommand.Create(playerId, request.ManagerId, request.Reason);

            if (commandResult.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));

            return Ok(new RevokeVipResponse(playerId));
        }
        catch (Exception exception)
        {
            return Problem(exception.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [NonAction]
    public override Task<ActionResult<RevokeVipResponse>> HandleAsync()
    {
        throw new NotSupportedException("Use the route overload with a player identifier.");
    }
}

public sealed record RevokeVipResponse(Guid PlayerId);
