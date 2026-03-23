using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using Xunit;

namespace UnitTests.Domain.Common.Values;

public class TimeRangeTests
{
    [Fact]
    public void TimeRange_WithSameValues_ShouldBeEqual()
    {
        DateTime start = new DateTime(2026, 1, 1, 8, 0, 0);
        DateTime end = new DateTime(2026, 1, 1, 16, 0, 0);

        TimeRange first = new TimeRange(start, end);
        TimeRange second = new TimeRange(start, end);

        Assert.Equal(first, second);
    }

    [Fact]
    public void TimeRange_WithDifferentValues_ShouldNotBeEqual()
    {
        TimeRange first = new TimeRange(
            new DateTime(2026, 1, 1, 8, 0, 0),
            new DateTime(2026, 1, 1, 16, 0, 0));

        TimeRange second = new TimeRange(
            new DateTime(2026, 1, 1, 9, 0, 0),
            new DateTime(2026, 1, 1, 17, 0, 0));

        Assert.NotEqual(first, second);
    }
}