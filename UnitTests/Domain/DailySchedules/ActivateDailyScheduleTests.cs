using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using Xunit;

namespace UnitTests.Domain.DailySchedules;

public class ActivateDailyScheduleTests
{
    [Fact]
    public void Activate_OnDraftDailySchedule_ShouldSucceed()
    {
        DailyScheduleId id = new DailyScheduleId(Guid.NewGuid());
        ManagerId managerId = new ManagerId(Guid.NewGuid());
        TimeRange window = new TimeRange(
            new DateTime(2026, 1, 1, 8, 0, 0),
            new DateTime(2026, 1, 1, 16, 0, 0));

        DailySchedule dailySchedule = DailySchedule.Create(id, managerId, window).Value!;

        Result result = dailySchedule.Activate();

        Assert.True(result.IsSuccess);
        Assert.Equal("Active", dailySchedule.Status);
    }

    [Fact]
    public void Activate_OnAlreadyActiveDailySchedule_ShouldFail()
    {
        DailyScheduleId id = new DailyScheduleId(Guid.NewGuid());
        ManagerId managerId = new ManagerId(Guid.NewGuid());
        TimeRange window = new TimeRange(
            new DateTime(2026, 1, 1, 8, 0, 0),
            new DateTime(2026, 1, 1, 16, 0, 0));

        DailySchedule dailySchedule = DailySchedule.Create(id, managerId, window).Value!;

        dailySchedule.Activate();
        Result result = dailySchedule.Activate();

        Assert.True(result.IsFailure);
    }
}