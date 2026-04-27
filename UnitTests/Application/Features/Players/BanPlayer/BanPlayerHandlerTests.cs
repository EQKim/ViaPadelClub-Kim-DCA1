using UnitTests.Fakes;
using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.BanPlayer;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RegisterPlayer;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.Players.BanPlayer;

public sealed class BanPlayerHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithExistingPlayer_ShouldBanPlayer()
    {
        FakePlayerRepository playerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        RegisterPlayerCommand registerCommand =
            RegisterPlayerCommand.Create(Guid.NewGuid(), "VIA University College").Value!;

        Player player = Player.Register(
            registerCommand.PlayerId,
            registerCommand.UniversityName).Value!;

        await playerRepository.AddAsync(player);

        ICommandHandler<BanPlayerCommand> handler =
            new BanPlayerHandler(playerRepository, unitOfWork);

        BanPlayerCommand command =
            BanPlayerCommand.Create(player.Id.Value).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsSuccess);
        Assert.True(player.IsBanned);
        Assert.True(unitOfWork.SaveChangesWasCalled);
    }

    [Fact]
    public async Task HandleAsync_WithUnknownPlayer_ShouldFail()
    {
        FakePlayerRepository playerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        ICommandHandler<BanPlayerCommand> handler =
            new BanPlayerHandler(playerRepository, unitOfWork);

        BanPlayerCommand command =
            BanPlayerCommand.Create(Guid.NewGuid()).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsFailure);
        Assert.False(unitOfWork.SaveChangesWasCalled);
    }
}