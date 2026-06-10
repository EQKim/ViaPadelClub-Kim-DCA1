using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Domain.Players;

public class BanPlayerTests
{
    [Fact]
    public void Ban_OnNonBannedPlayer_ShouldSucceed()
    {
        PlayerId playerId = new PlayerId(Guid.NewGuid());
        UniversityName universityName = new UniversityName("VIA University College");

        Result<Player> registerResult = Player.Register(playerId, universityName);
        Player player = registerResult.Value!;

        Result result = player.Ban();

        Assert.True(result.IsSuccess);
        Assert.True(player.IsBanned);
    }

    [Fact]
    public void Ban_OnAlreadyBannedPlayer_ShouldFail()
    {
        PlayerId playerId = new PlayerId(Guid.NewGuid());
        UniversityName universityName = new UniversityName("VIA University College");

        Result<Player> registerResult = Player.Register(playerId, universityName);
        Player player = registerResult.Value!;

        player.Ban();
        Result result = player.Ban();

        Assert.True(result.IsFailure);
    }
}