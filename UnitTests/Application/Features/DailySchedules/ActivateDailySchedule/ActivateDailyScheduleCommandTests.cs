using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.ActivateDailySchedule;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.DailySchedules.ActivateDailySchedule;

public sealed class ActivateDailyScheduleCommandTests
{
    [Fact]
    public void Create_WithValidInput_ShouldCreateCommand()
    {
        Guid dailyScheduleId = Guid.NewGuid();

        Result<ActivateDailyScheduleCommand> result =
            ActivateDailyScheduleCommand.Create(dailyScheduleId);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(dailyScheduleId, result.Value.DailyScheduleId.Value);
    }

    [Fact]
    public void Create_WithEmptyDailyScheduleId_ShouldFail()
    {
        Result<ActivateDailyScheduleCommand> result =
            ActivateDailyScheduleCommand.Create(Guid.Empty);

        Assert.True(result.IsFailure);
    }
}