using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RegisterPlayer;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Players;

[ApiController]
[Route("api/players")]
public sealed class RegisterPlayerEndpoint
    : ApiEndpoint.WithRequest<RegisterPlayerRequest>.WithResponse<RegisterPlayerResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RegisterPlayerEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("register")]
    public override async Task<ActionResult<RegisterPlayerResponse>> HandleAsync(
        [FromBody] RegisterPlayerRequest request)
    {
        try
        {
            Result<RegisterPlayerCommand> commandResult =
                RegisterPlayerCommand.Create(request.PlayerId, request.UniversityName);

            if (commandResult.IsFailure)
            {
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));
            }

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
            {
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));
            }

            return Ok(new RegisterPlayerResponse(request.PlayerId));
        }
        catch (Exception exception)
        {
            return Problem(ExceptionResponseFactory.GetDetail(exception), statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}

public sealed record RegisterPlayerRequest(Guid PlayerId, string UniversityName);

public sealed record RegisterPlayerResponse(Guid PlayerId);
