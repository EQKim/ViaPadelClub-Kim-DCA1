using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using Xunit;

namespace UnitTests.Domain.Common.Values;

public class PlayerIdTests
{
    [Fact]
    public void PlayerId_WithSameGuid_ShouldBeEqual()
    {
        Guid guid = Guid.NewGuid();
        PlayerId first = new PlayerId(guid);
        PlayerId second = new PlayerId(guid);
        Assert.Equal(first, second);
    }

    [Fact]
    public void PlayerId_WithDifferentGuid_ShouldNotBeEqual()
    {
        PlayerId first = new PlayerId(Guid.NewGuid());
        PlayerId second = new PlayerId(Guid.NewGuid());
        Assert.NotEqual(first, second);
    }
}