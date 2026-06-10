using UnitTests.Fakes;
using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CancelBooking;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.DailySchedules;

public sealed class CancelBookingHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithExistingBooking_ShouldCancelBooking()
    {
        FakeDailyScheduleRepository dailyScheduleRepository = new();
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

        Booking booking = court.CreateBooking(
            player,
            new BookingId(Guid.NewGuid()),
            new TimeRange(DateTime.Today.AddHours(10), DateTime.Today.AddHours(11)),
            dailySchedule.Window).Value!;

        await dailyScheduleRepository.AddAsync(dailySchedule);

        ICommandHandler<CancelBookingCommand> handler =
            new CancelBookingHandler(dailyScheduleRepository, unitOfWork);

        CancelBookingCommand command =
            CancelBookingCommand.Create(
                dailySchedule.Id.Value,
                court.Id.Value,
                booking.Id.Value).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsSuccess);
        Assert.Equal("Cancelled", booking.Status);
        Assert.True(unitOfWork.SaveChangesWasCalled);
    }

    [Fact]
    public async Task HandleAsync_WithUnknownBooking_ShouldFail()
    {
        FakeDailyScheduleRepository dailyScheduleRepository = new();
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

        ICommandHandler<CancelBookingCommand> handler =
            new CancelBookingHandler(dailyScheduleRepository, unitOfWork);

        CancelBookingCommand command =
            CancelBookingCommand.Create(
                dailySchedule.Id.Value,
                court.Id.Value,
                Guid.NewGuid()).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsFailure);
        Assert.False(unitOfWork.SaveChangesWasCalled);
    }
}