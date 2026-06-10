using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RegisterPlayer;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Players;

public sealed class RegisterPlayerCommandTests
{
    [Fact]
    public void Create_WithValidInput_ShouldCreateCommand()
    {
        Guid playerId = Guid.NewGuid();
        string universityName = "VIA University College";

        Result<RegisterPlayerCommand> result = RegisterPlayerCommand.Create(playerId, universityName);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(playerId, result.Value.PlayerId.Value);
        Assert.Equal(universityName, result.Value.UniversityName.Value);
    }

    [Fact]
    public void Create_WithEmptyUniversityName_ShouldFail()
    {
        Result<RegisterPlayerCommand> result = RegisterPlayerCommand.Create(Guid.NewGuid(), "");

        Assert.True(result.IsFailure);
    }
}