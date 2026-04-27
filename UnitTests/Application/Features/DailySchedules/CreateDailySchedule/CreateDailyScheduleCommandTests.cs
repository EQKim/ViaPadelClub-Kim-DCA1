using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateDailySchedule;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.DailySchedules.CreateDailySchedule;

public sealed class CreateDailyScheduleCommandTests
{
    [Fact]
    public void Create_WithValidInput_ShouldCreateCommand()
    {
        Guid dailyScheduleId = Guid.NewGuid();
        Guid managerId = Guid.NewGuid();
        DateTime start = DateTime.Today.AddHours(8);
        DateTime end = DateTime.Today.AddHours(22);

        Result<CreateDailyScheduleCommand> result =
            CreateDailyScheduleCommand.Create(dailyScheduleId, managerId, start, end);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(dailyScheduleId, result.Value.DailyScheduleId.Value);
        Assert.Equal(managerId, result.Value.ManagerId.Value);
        Assert.Equal(start, result.Value.Window.Start);
        Assert.Equal(end, result.Value.Window.End);
    }

    [Fact]
    public void Create_WithInvalidTimeRange_ShouldFail()
    {
        DateTime start = DateTime.Today.AddHours(22);
        DateTime end = DateTime.Today.AddHours(8);

        Result<CreateDailyScheduleCommand> result =
            CreateDailyScheduleCommand.Create(Guid.NewGuid(), Guid.NewGuid(), start, end);

        Assert.True(result.IsFailure);
    }
}