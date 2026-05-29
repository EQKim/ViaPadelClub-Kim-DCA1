using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.GrantVip;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Players;

[ApiController]
[Route("api/players")]
public sealed class GrantVipEndpoint
    : ApiEndpoint.WithoutRequest.WithResponse<GrantVipResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public GrantVipEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{playerId:guid}/grant-vip")]
    public async Task<ActionResult<GrantVipResponse>> HandleAsync(
        Guid playerId,
        [FromBody] PlayerAdminActionRequest request)
    {
        try
        {
            Result<GrantVipCommand> commandResult =
                GrantVipCommand.Create(playerId, request.ManagerId, request.Reason);

            if (commandResult.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));

            return Ok(new GrantVipResponse(playerId));
        }
        catch (Exception exception)
        {
            return Problem(exception.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [NonAction]
    public override Task<ActionResult<GrantVipResponse>> HandleAsync()
    {
        throw new NotSupportedException("Use the route overload with a player identifier.");
    }
}

public sealed record GrantVipResponse(Guid PlayerId);
