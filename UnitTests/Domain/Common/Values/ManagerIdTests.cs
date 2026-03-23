using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using Xunit;

namespace UnitTests.Domain.Common.Values;

public class ManagerIdTests
{
    [Fact]
    public void ManagerId_WithSameGuid_ShouldBeEqual()
    {
        Guid guid = Guid.NewGuid();

        ManagerId first = new ManagerId(guid);
        ManagerId second = new ManagerId(guid);

        Assert.Equal(first, second);
    }

    [Fact]
    public void ManagerId_WithDifferentGuid_ShouldNotBeEqual()
    {
        ManagerId first = new ManagerId(Guid.NewGuid());
        ManagerId second = new ManagerId(Guid.NewGuid());

        Assert.NotEqual(first, second);
    }
}