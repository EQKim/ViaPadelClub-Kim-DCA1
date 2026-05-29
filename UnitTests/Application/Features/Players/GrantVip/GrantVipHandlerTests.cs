using UnitTests.Fakes;
using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.GrantVip;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RegisterPlayer;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.Players.GrantVip;

public sealed class GrantVipHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithExistingPlayer_ShouldGrantVip()
    {
        FakePlayerRepository playerRepository = new();
        FakeManagerRepository managerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        RegisterPlayerCommand registerCommand =
            RegisterPlayerCommand.Create(Guid.NewGuid(), "VIA University College").Value!;

        Player player = Player.Register(
            registerCommand.PlayerId,
            registerCommand.UniversityName).Value!;

        await playerRepository.AddAsync(player);

        ICommandHandler<GrantVipCommand> handler =
            new GrantVipHandler(playerRepository, managerRepository, unitOfWork);

        GrantVipCommand command =
            GrantVipCommand.Create(player.Id.Value).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsSuccess);
        Assert.True(player.IsVip);
        Assert.True(unitOfWork.SaveChangesWasCalled);
    }

    [Fact]
    public async Task HandleAsync_WithUnknownPlayer_ShouldFail()
    {
        FakePlayerRepository playerRepository = new();
        FakeManagerRepository managerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        ICommandHandler<GrantVipCommand> handler =
            new GrantVipHandler(playerRepository, managerRepository, unitOfWork);

        GrantVipCommand command =
            GrantVipCommand.Create(Guid.NewGuid()).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsFailure);
        Assert.False(unitOfWork.SaveChangesWasCalled);
    }
}
