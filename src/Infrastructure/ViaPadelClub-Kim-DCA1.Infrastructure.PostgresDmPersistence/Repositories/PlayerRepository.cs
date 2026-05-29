using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Repositories;

public sealed class PlayerRepository : Repository<Player, PlayerId>, IPlayerRepository
{
    public PlayerRepository(DmContext context) : base(context)
    {
    }

    public override Task<Player?> GetByIdAsync(PlayerId id)
    {
        return Context.Players
            .Include(player => player.AdminActions)
            .FirstOrDefaultAsync(player => player.Id == id);
    }
}
