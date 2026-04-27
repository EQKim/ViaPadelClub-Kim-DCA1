using UnitTests.Fakes;
using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateDailySchedule;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.DailySchedules.CreateDailySchedule;

public sealed class CreateDailyScheduleHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithValidCommand_ShouldCreateDailySchedule()
    {
        FakeDailyScheduleRepository dailyScheduleRepository = new();
        FakeUnitOfWork unitOfWork = new();

        ICommandHandler<CreateDailyScheduleCommand> handler =
            new CreateDailyScheduleHandler(dailyScheduleRepository, unitOfWork);

        DateTime start = DateTime.Today.AddHours(8);
        DateTime end = DateTime.Today.AddHours(22);

        CreateDailyScheduleCommand command =
            CreateDailyScheduleCommand.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                start,
                end).Value!;

        Result result = await handler.HandleAsync(command);

        Assert.True(result.IsSuccess);
        Assert.Single(dailyScheduleRepository.DailySchedules);
        Assert.True(unitOfWork.SaveChangesWasCalled);

        DailySchedule dailySchedule = dailyScheduleRepository.DailySchedules.First();

        Assert.Equal(command.DailyScheduleId, dailySchedule.Id);
        Assert.Equal(command.ManagerId, dailySchedule.ManagerId);
        Assert.Equal(command.Window, dailySchedule.Window);
        Assert.Equal("Draft", dailySchedule.Status);
    }
}