using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.Players;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Handlers;

public sealed class GetPlayerDirectoryQueryHandler
    : IQueryHandler<GetPlayerDirectoryQuery, PlayerDirectoryAnswer>
{
    private readonly QueryDbContext _context;

    public GetPlayerDirectoryQueryHandler(QueryDbContext context)
    {
        _context = context;
    }

    public async Task<PlayerDirectoryAnswer> HandleAsync(GetPlayerDirectoryQuery query)
    {
        IQueryable<Models.PlayerReadModel> players = _context.Players.AsNoTracking();

        if (query.IsVip is not null)
        {
            players = players.Where(player => player.IsVip == query.IsVip.Value);
        }

        if (query.IsBanned is not null)
        {
            players = players.Where(player => player.IsBanned == query.IsBanned.Value);
        }

        List<PlayerDirectoryItemDto> result = await players
            .OrderBy(player => player.UniversityName)
            .ThenBy(player => player.Id)
            .Select(player => new PlayerDirectoryItemDto(
                player.Id,
                player.UniversityName,
                player.IsVip,
                player.IsBanned))
            .ToListAsync();

        return new PlayerDirectoryAnswer(result);
    }
}
