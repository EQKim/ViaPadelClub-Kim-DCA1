using UnitTests.Fakes;
using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.BanPlayer;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RegisterPlayer;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Players;

public sealed class BanPlayerHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithExistingPlayer_ShouldBanPlayer()
    {
        FakePlayerRepository playerRepository = new();
        FakeDailyScheduleRepository dailyScheduleRepository = new();
        FakeManagerRepository managerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        RegisterPlayerCommand registerCommand =
            RegisterPlayerCommand.Create(Guid.NewGuid(), "VIA University College").Value!;

        Player player = Player.Register(
            registerCommand.PlayerId,
            registerCommand.UniversityName).Value!;

        await playerRepository.AddAsync(player);

        ICommandHandler<BanPlayerCommand> handler =
            new BanPlayerHandler(playerRepository, dailyScheduleRepository, managerRepository, unitOfWork);

        BanPlayerCommand command =
            BanPlayerCommand.Create(player.Id.Value).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsSuccess);
        Assert.True(player.IsBanned);
        Assert.True(unitOfWork.SaveChangesWasCalled);
    }

    [Fact]
    public async Task HandleAsync_WithFutureActiveBookings_ShouldCancelBookings()
    {
        FakePlayerRepository playerRepository = new();
        FakeDailyScheduleRepository dailyScheduleRepository = new();
        FakeManagerRepository managerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        Player player = Player.Register(
            new ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values.PlayerId(Guid.NewGuid()),
            new ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values.UniversityName("VIA University College")).Value!;

        DateTime start = DateTime.UtcNow.AddHours(1);
        TimeRange window = new(start, start.AddHours(4));
        DailySchedule dailySchedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            window).Value!;
        dailySchedule.Activate();

        DailyScheduleCourt court = dailySchedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            false);

        Booking booking = dailySchedule.CreateBooking(
            player,
            court.Id,
            new BookingId(Guid.NewGuid()),
            new TimeRange(start.AddHours(1), start.AddHours(2))).Value!;

        await playerRepository.AddAsync(player);
        await dailyScheduleRepository.AddAsync(dailySchedule);

        ICommandHandler<BanPlayerCommand> handler =
            new BanPlayerHandler(playerRepository, dailyScheduleRepository, managerRepository, unitOfWork);

        BanPlayerCommand command = BanPlayerCommand.Create(player.Id.Value).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsSuccess);
        Assert.True(player.IsBanned);
        Assert.Equal("Cancelled", booking.Status);
        Assert.True(unitOfWork.SaveChangesWasCalled);
    }

    [Fact]
    public async Task HandleAsync_WithUnknownPlayer_ShouldFail()
    {
        FakePlayerRepository playerRepository = new();
        FakeDailyScheduleRepository dailyScheduleRepository = new();
        FakeManagerRepository managerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        ICommandHandler<BanPlayerCommand> handler =
            new BanPlayerHandler(playerRepository, dailyScheduleRepository, managerRepository, unitOfWork);

        BanPlayerCommand command =
            BanPlayerCommand.Create(Guid.NewGuid()).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsFailure);
        Assert.False(unitOfWork.SaveChangesWasCalled);
    }
}
