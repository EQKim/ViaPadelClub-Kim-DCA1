using UnitTests.Fakes;
using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RegisterPlayer;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RevokeVip;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.Players.RevokeVip;

public sealed class RevokeVipHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithVipPlayer_ShouldRevokeVip()
    {
        FakePlayerRepository playerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        RegisterPlayerCommand registerCommand =
            RegisterPlayerCommand.Create(Guid.NewGuid(), "VIA University College").Value!;

        Player player = Player.Register(
            registerCommand.PlayerId,
            registerCommand.UniversityName).Value!;

        player.GrantVip();

        await playerRepository.AddAsync(player);

        ICommandHandler<RevokeVipCommand> handler =
            new RevokeVipHandler(playerRepository, unitOfWork);

        RevokeVipCommand command =
            RevokeVipCommand.Create(player.Id.Value).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsSuccess);
        Assert.False(player.IsVip);
        Assert.True(unitOfWork.SaveChangesWasCalled);
    }

    [Fact]
    public async Task HandleAsync_WithUnknownPlayer_ShouldFail()
    {
        FakePlayerRepository playerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        ICommandHandler<RevokeVipCommand> handler =
            new RevokeVipHandler(playerRepository, unitOfWork);

        RevokeVipCommand command =
            RevokeVipCommand.Create(Guid.NewGuid()).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsFailure);
        Assert.False(unitOfWork.SaveChangesWasCalled);
    }
}