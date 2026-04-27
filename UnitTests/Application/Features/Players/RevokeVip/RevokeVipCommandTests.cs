using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RevokeVip;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.Players.RevokeVip;

public sealed class RevokeVipCommandTests
{
    [Fact]
    public void Create_WithValidInput_ShouldCreateCommand()
    {
        Guid playerId = Guid.NewGuid();

        Result<RevokeVipCommand> result = RevokeVipCommand.Create(playerId);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(playerId, result.Value.PlayerId.Value);
    }

    [Fact]
    public void Create_WithEmptyPlayerId_ShouldFail()
    {
        Result<RevokeVipCommand> result = RevokeVipCommand.Create(Guid.Empty);

        Assert.True(result.IsFailure);
    }
}