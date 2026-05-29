using UnitTests.Fakes;
using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateBooking;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.DailySchedules.CreateBooking;

public sealed class CreateBookingHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithValidCommand_ShouldCreateBooking()
    {
        FakeDailyScheduleRepository dailyScheduleRepository = new();
        FakePlayerRepository playerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        TimeRange window = new(DateTime.Today.AddHours(8), DateTime.Today.AddHours(22));

        DailySchedule dailySchedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            window).Value!;
        dailySchedule.Activate();

        DailyScheduleCourt court = dailySchedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            false);

        Player player = Player.Register(
            new PlayerId(Guid.NewGuid()),
            new UniversityName("VIA University College")).Value!;

        await dailyScheduleRepository.AddAsync(dailySchedule);
        await playerRepository.AddAsync(player);

        ICommandHandler<CreateBookingCommand> handler =
            new CreateBookingHandler(dailyScheduleRepository, playerRepository, unitOfWork);

        CreateBookingCommand command =
            CreateBookingCommand.Create(
                dailySchedule.Id.Value,
                court.Id.Value,
                Guid.NewGuid(),
                player.Id.Value,
                DateTime.Today.AddHours(10),
                DateTime.Today.AddHours(11)).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsSuccess);
        Assert.Single(court.Bookings);
        Assert.True(unitOfWork.SaveChangesWasCalled);
    }

    [Fact]
    public async Task HandleAsync_WithDraftSchedule_ShouldFail()
    {
        FakeDailyScheduleRepository dailyScheduleRepository = new();
        FakePlayerRepository playerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        TimeRange window = new(DateTime.Today.AddHours(8), DateTime.Today.AddHours(22));

        DailySchedule dailySchedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            window).Value!;

        DailyScheduleCourt court = dailySchedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            false);

        Player player = Player.Register(
            new PlayerId(Guid.NewGuid()),
            new UniversityName("VIA University College")).Value!;

        await dailyScheduleRepository.AddAsync(dailySchedule);
        await playerRepository.AddAsync(player);

        ICommandHandler<CreateBookingCommand> handler =
            new CreateBookingHandler(dailyScheduleRepository, playerRepository, unitOfWork);

        CreateBookingCommand command =
            CreateBookingCommand.Create(
                dailySchedule.Id.Value,
                court.Id.Value,
                Guid.NewGuid(),
                player.Id.Value,
                DateTime.Today.AddHours(10),
                DateTime.Today.AddHours(11)).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "dailyschedule.not_active");
        Assert.Empty(court.Bookings);
        Assert.False(unitOfWork.SaveChangesWasCalled);
    }

    [Fact]
    public async Task HandleAsync_WithUnknownPlayer_ShouldFail()
    {
        FakeDailyScheduleRepository dailyScheduleRepository = new();
        FakePlayerRepository playerRepository = new();
        FakeUnitOfWork unitOfWork = new();

        TimeRange window = new(DateTime.Today.AddHours(8), DateTime.Today.AddHours(22));

        DailySchedule dailySchedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            window).Value!;

        DailyScheduleCourt court = dailySchedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            false);

        await dailyScheduleRepository.AddAsync(dailySchedule);

        ICommandHandler<CreateBookingCommand> handler =
            new CreateBookingHandler(dailyScheduleRepository, playerRepository, unitOfWork);

        CreateBookingCommand command =
            CreateBookingCommand.Create(
                dailySchedule.Id.Value,
                court.Id.Value,
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.Today.AddHours(10),
                DateTime.Today.AddHours(11)).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsFailure);
        Assert.False(unitOfWork.SaveChangesWasCalled);
    }
}
