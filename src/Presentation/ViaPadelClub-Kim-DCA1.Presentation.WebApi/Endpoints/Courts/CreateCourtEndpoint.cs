using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Courts.CreateCourt;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Courts;

[ApiController]
[Route("api/courts")]
public sealed class CreateCourtEndpoint
    : ApiEndpoint.WithRequest<CreateCourtRequest>.WithResponse<CreateCourtResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public CreateCourtEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public override async Task<ActionResult<CreateCourtResponse>> HandleAsync(
        [FromBody] CreateCourtRequest request)
    {
        try
        {
            Result<CreateCourtCommand> commandResult =
                CreateCourtCommand.Create(request.CourtId, request.CourtName);

            if (commandResult.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));

            return Ok(new CreateCourtResponse(request.CourtId));
        }
        catch (Exception exception)
        {
            return Problem(exception.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}

public sealed record CreateCourtRequest(Guid CourtId, string CourtName);

public sealed record CreateCourtResponse(Guid CourtId);
