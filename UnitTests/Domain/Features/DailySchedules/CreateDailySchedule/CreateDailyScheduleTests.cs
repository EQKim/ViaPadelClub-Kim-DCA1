using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using Xunit;

namespace UnitTests.Features.DailySchedules.CreateDailySchedule;

public class CreateDailyScheduleTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateDraftDailySchedule()
    {
        DailyScheduleId id = new DailyScheduleId(Guid.NewGuid());
        ManagerId managerId = new ManagerId(Guid.NewGuid());
        TimeRange window = new TimeRange(
            new DateTime(2026, 1, 1, 8, 0, 0),
            new DateTime(2026, 1, 1, 16, 0, 0));

        Result<DailySchedule> result = DailySchedule.Create(id, managerId, window);

        Assert.True(result.IsSuccess);

        DailySchedule dailySchedule = result.Value;

        Assert.Equal(id, dailySchedule.Id);
        Assert.Equal(managerId, dailySchedule.ManagerId);
        Assert.Equal(window, dailySchedule.Window);
        Assert.Equal("Draft", dailySchedule.Status);
    }

    [Fact]
    public void Create_WithEndBeforeStart_ShouldFail()
    {
        DailyScheduleId id = new DailyScheduleId(Guid.NewGuid());
        ManagerId managerId = new ManagerId(Guid.NewGuid());
        TimeRange window = new TimeRange(
            new DateTime(2026, 1, 1, 16, 0, 0),
            new DateTime(2026, 1, 1, 8, 0, 0));

        Result<DailySchedule> result = DailySchedule.Create(id, managerId, window);

        Assert.True(result.IsFailure);
    }
}