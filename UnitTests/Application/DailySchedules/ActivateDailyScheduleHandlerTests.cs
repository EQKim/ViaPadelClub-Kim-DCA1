using UnitTests.Fakes;
using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.ActivateDailySchedule;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateDailySchedule;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.DailySchedules;

public sealed class ActivateDailyScheduleHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithExistingDraftSchedule_ShouldActivateSchedule()
    {
        FakeDailyScheduleRepository dailyScheduleRepository = new();
        FakeUnitOfWork unitOfWork = new();

        DateTime start = DateTime.Today.AddHours(8);
        DateTime end = DateTime.Today.AddHours(22);

        CreateDailyScheduleCommand createCommand =
            CreateDailyScheduleCommand.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                start,
                end).Value!;

        DailySchedule dailySchedule = DailySchedule.Create(
            createCommand.DailyScheduleId,
            createCommand.ManagerId,
            createCommand.Window).Value!;

        await dailyScheduleRepository.AddAsync(dailySchedule);

        ICommandHandler<ActivateDailyScheduleCommand> handler =
            new ActivateDailyScheduleHandler(dailyScheduleRepository, unitOfWork);

        ActivateDailyScheduleCommand command =
            ActivateDailyScheduleCommand.Create(dailySchedule.Id.Value).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsSuccess);
        Assert.Equal("Active", dailySchedule.Status);
        Assert.True(unitOfWork.SaveChangesWasCalled);
    }

    [Fact]
    public async Task HandleAsync_WithUnknownSchedule_ShouldFail()
    {
        FakeDailyScheduleRepository dailyScheduleRepository = new();
        FakeUnitOfWork unitOfWork = new();

        ICommandHandler<ActivateDailyScheduleCommand> handler =
            new ActivateDailyScheduleHandler(dailyScheduleRepository, unitOfWork);

        ActivateDailyScheduleCommand command =
            ActivateDailyScheduleCommand.Create(Guid.NewGuid()).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsFailure);
        Assert.False(unitOfWork.SaveChangesWasCalled);
    }
}