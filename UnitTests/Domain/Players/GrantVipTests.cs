using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Domain.Players;

public class GrantVipTests
{
    [Fact]
    public void GrantVip_OnNonVipPlayer_ShouldSucceed()
    {
        PlayerId playerId = new PlayerId(Guid.NewGuid());
        UniversityName universityName = new UniversityName("VIA University College");

        Result<Player> registerResult = Player.Register(playerId, universityName);
        Player player = registerResult.Value;

        Result result = player.GrantVip();

        Assert.True(result.IsSuccess);
        Assert.True(player.IsVip);
    }

    [Fact]
    public void GrantVip_OnAlreadyVipPlayer_ShouldFail()
    {
        PlayerId playerId = new PlayerId(Guid.NewGuid());
        UniversityName universityName = new UniversityName("VIA University College");

        Result<Player> registerResult = Player.Register(playerId, universityName);
        Player player = registerResult.Value;

        player.GrantVip();
        Result result = player.GrantVip();

        Assert.True(result.IsFailure);
    }
}