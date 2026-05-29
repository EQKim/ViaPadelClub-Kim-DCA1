using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.Players;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Players;

[ApiController]
[Route("api/players")]
public sealed class GetPlayerDirectoryEndpoint
    : ApiEndpoint.WithRequest<GetPlayerDirectoryRequest>.WithResponse<GetPlayerDirectoryResponse>
{
    private readonly IQueryDispatcher _queryDispatcher;

    public GetPlayerDirectoryEndpoint(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public override async Task<ActionResult<GetPlayerDirectoryResponse>> HandleAsync(
        [FromQuery] GetPlayerDirectoryRequest request)
    {
        try
        {
            PlayerDirectoryAnswer answer =
                await _queryDispatcher.DispatchAsync<GetPlayerDirectoryQuery, PlayerDirectoryAnswer>(
                    new GetPlayerDirectoryQuery(request.IsVip, request.IsBanned));

            GetPlayerDirectoryResponse response = new(answer.Players
                .Select(player => new PlayerDirectoryItemResponse(
                    player.PlayerId,
                    player.UniversityName,
                    player.IsVip,
                    player.IsBanned))
                .ToList());

            return Ok(response);
        }
        catch (Exception exception)
        {
            return Problem(ExceptionResponseFactory.GetDetail(exception), statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}

public sealed record GetPlayerDirectoryRequest(bool? IsVip = null, bool? IsBanned = null);

public sealed record GetPlayerDirectoryResponse(IReadOnlyList<PlayerDirectoryItemResponse> Players);

public sealed record PlayerDirectoryItemResponse(
    Guid PlayerId,
    string UniversityName,
    bool IsVip,
    bool IsBanned);
