using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Managers.CreateManager;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Managers;

[ApiController]
[Route("api/managers")]
public sealed class CreateManagerEndpoint
    : ApiEndpoint.WithRequest<CreateManagerRequest>.WithResponse<CreateManagerResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public CreateManagerEndpoint(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public override async Task<ActionResult<CreateManagerResponse>> HandleAsync(
        [FromBody] CreateManagerRequest request)
    {
        try
        {
            Result<CreateManagerCommand> commandResult =
                CreateManagerCommand.Create(request.ManagerId, request.PadelCompanyName);

            if (commandResult.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(commandResult.Errors));

            Result result = await _commandDispatcher.DispatchAsync(commandResult.Value!);

            if (result.IsFailure)
                return BadRequest(ResultResponseFactory.FromErrors(result.Errors));

            return Ok(new CreateManagerResponse(request.ManagerId));
        }
        catch (Exception exception)
        {
            return Problem(ExceptionResponseFactory.GetDetail(exception), statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}

public sealed record CreateManagerRequest(Guid ManagerId, string PadelCompanyName);

public sealed record CreateManagerResponse(Guid ManagerId);
