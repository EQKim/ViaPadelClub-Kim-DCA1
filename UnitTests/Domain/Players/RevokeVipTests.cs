using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using Xunit;

namespace UnitTests.Domain.Players;

public class RevokeVipTests
{
    [Fact]
    public void RevokeVip_OnVipPlayer_ShouldSucceed()
    {
        PlayerId id = new PlayerId(Guid.NewGuid());
        UniversityName university = new UniversityName("VIA");

        Player player = Player.Register(id, university).Value;
        player.GrantVip();

        Result result = player.RevokeVip();

        Assert.True(result.IsSuccess);
        Assert.False(player.IsVip);
    }

    [Fact]
    public void RevokeVip_OnNonVipPlayer_ShouldFail()
    {
        PlayerId id = new PlayerId(Guid.NewGuid());
        UniversityName university = new UniversityName("VIA");

        Player player = Player.Register(id, university).Value;

        Result result = player.RevokeVip();

        Assert.True(result.IsFailure);
    }
}