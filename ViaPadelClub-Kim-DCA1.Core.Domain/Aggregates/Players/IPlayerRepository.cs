using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;

public interface IPlayerRepository
{
    Task AddAsync(Player player);
    Task<Player?> GetByIdAsync(PlayerId id);
}