using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;

namespace UnitTests.Fakes;

public sealed class FakePlayerRepository : IPlayerRepository
{
    private readonly List<Player> _players = new();

    public IReadOnlyList<Player> Players => _players;

    public Task AddAsync(Player player)
    {
        _players.Add(player);
        return Task.CompletedTask;
    }

    public Task<Player?> GetByIdAsync(PlayerId id)
    {
        Player? player = _players.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(player);
    }
}