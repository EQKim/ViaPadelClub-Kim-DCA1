using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Repositories;

public sealed class PlayerRepository : Repository<Player, PlayerId>, IPlayerRepository
{
    public PlayerRepository(DmContext context) : base(context)
    {
    }
}
