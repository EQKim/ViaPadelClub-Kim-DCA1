using Microsoft.AspNetCore.Mvc;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.Courts;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Common;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Courts;

[ApiController]
[Route("api/courts")]
public sealed class GetCourtsEndpoint : ApiEndpoint.WithoutRequest.WithResponse<GetCourtsResponse>
{
    private readonly IQueryDispatcher _queryDispatcher;

    public GetCourtsEndpoint(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public override async Task<ActionResult<GetCourtsResponse>> HandleAsync()
    {
        try
        {
            CourtsAnswer answer = await _queryDispatcher.DispatchAsync<GetCourtsQuery, CourtsAnswer>(new GetCourtsQuery());
            GetCourtsResponse response = new(answer.Courts
                .Select(court => new CourtResponseItem(court.CourtId, court.CourtName))
                .ToList());

            return Ok(response);
        }
        catch (Exception exception)
        {
            return Problem(ExceptionResponseFactory.GetDetail(exception), statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}

public sealed record GetCourtsResponse(IReadOnlyList<CourtResponseItem> Courts);

public sealed record CourtResponseItem(Guid CourtId, string CourtName);
