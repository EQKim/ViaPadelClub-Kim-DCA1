using UnitTests.Fakes;
using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RegisterPlayer;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.Players.RegisterPlayer;

public sealed class RegisterPlayerHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithValidCommand_ShouldRegisterPlayer()
    {
        FakePlayerRepository playerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        ICommandHandler<RegisterPlayerCommand> handler =
            new RegisterPlayerHandler(playerRepository, unitOfWork);

        RegisterPlayerCommand command =
            RegisterPlayerCommand.Create(Guid.NewGuid(), "VIA University College").Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsSuccess);
        Assert.Single(playerRepository.Players);
        Assert.True(unitOfWork.SaveChangesWasCalled);

        Player player = playerRepository.Players.First();

        Assert.Equal(command.PlayerId, player.Id);
        Assert.Equal(command.UniversityName, player.UniversityName);
        Assert.False(player.IsVip);
        Assert.False(player.IsBanned);
    }
}