using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using Xunit;

namespace UnitTests.Features.Players.RegisterPlayer;

public class RegisterPlayerTests
{
    [Fact]
    public void Register_WithValidPlayerIdAndUniversityName_ShouldCreatePlayer()
    {
        PlayerId playerId = new PlayerId(Guid.NewGuid());
        UniversityName universityName = new UniversityName("VIA University College");

        Result<Player> result = Player.Register(playerId, universityName);

        Assert.True(result.IsSuccess);

        Player player = result.Value;

        Assert.Equal(playerId, player.Id);
        Assert.Equal(universityName, player.UniversityName);
        Assert.False(player.IsVip);
        Assert.False(player.IsBanned);
    }
    
    
    [Fact]
    public void Register_WithEmptyUniversityName_ShouldFail()
    {
        PlayerId playerId = new PlayerId(Guid.NewGuid());
        UniversityName universityName = new UniversityName("");

        Result<Player> result = Player.Register(playerId, universityName);
        Assert.True(result.IsFailure);
    }
}