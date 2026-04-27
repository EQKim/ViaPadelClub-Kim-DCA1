using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.BanPlayer;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.Players.BanPlayer;

public sealed class BanPlayerCommandTests
{
    [Fact]
    public void Create_WithValidInput_ShouldCreateCommand()
    {
        Guid playerId = Guid.NewGuid();

        Result<BanPlayerCommand> result = BanPlayerCommand.Create(playerId);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(playerId, result.Value.PlayerId.Value);
    }

    [Fact]
    public void Create_WithEmptyPlayerId_ShouldFail()
    {
        Result<BanPlayerCommand> result = BanPlayerCommand.Create(Guid.Empty);

        Assert.True(result.IsFailure);
    }
}