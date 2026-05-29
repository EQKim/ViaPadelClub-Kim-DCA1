using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.Courts;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Handlers;

public sealed class GetCourtsQueryHandler : IQueryHandler<GetCourtsQuery, CourtsAnswer>
{
    private readonly QueryDbContext _context;

    public GetCourtsQueryHandler(QueryDbContext context)
    {
        _context = context;
    }

    public async Task<CourtsAnswer> HandleAsync(GetCourtsQuery query)
    {
        List<CourtDto> courts = await _context.Courts
            .AsNoTracking()
            .OrderBy(court => court.CourtName)
            .ThenBy(court => court.Id)
            .Select(court => new CourtDto(court.Id, court.CourtName))
            .ToListAsync();

        return new CourtsAnswer(courts);
    }
}
