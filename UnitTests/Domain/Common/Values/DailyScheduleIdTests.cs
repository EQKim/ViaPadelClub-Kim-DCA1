using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using Xunit;

namespace UnitTests.Domain.Common.Values;

public class DailyScheduleIdTests
{
    [Fact]
    public void DailyScheduleId_WithSameGuid_ShouldBeEqual()
    {
        Guid guid = Guid.NewGuid();

        DailyScheduleId first = new DailyScheduleId(guid);
        DailyScheduleId second = new DailyScheduleId(guid);

        Assert.Equal(first, second);
    }

    [Fact]
    public void DailyScheduleId_WithDifferentGuid_ShouldNotBeEqual()
    {
        DailyScheduleId first = new DailyScheduleId(Guid.NewGuid());
        DailyScheduleId second = new DailyScheduleId(Guid.NewGuid());

        Assert.NotEqual(first, second);
    }
}