using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;

public interface IPlayerRepository : IRepository<Player, PlayerId>
{
}
