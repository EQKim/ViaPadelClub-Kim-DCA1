using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.GrantVip;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Players;

public sealed class GrantVipCommandTests
{
    [Fact]
    public void Create_WithValidInput_ShouldCreateCommand()
    {
        Guid playerId = Guid.NewGuid();

        Result<GrantVipCommand> result = GrantVipCommand.Create(playerId);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(playerId, result.Value.PlayerId.Value);
    }

    [Fact]
    public void Create_WithEmptyPlayerId_ShouldFail()
    {
        Result<GrantVipCommand> result = GrantVipCommand.Create(Guid.Empty);

        Assert.True(result.IsFailure);
    }
}