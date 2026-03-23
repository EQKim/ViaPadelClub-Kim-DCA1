using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using Xunit;

namespace UnitTests.Domain.Common.Values;

public class UniversityNameTests
{
    [Fact]
    public void UniversityName_WithSameValue_ShouldBeEqual()
    {
        UniversityName first = new UniversityName("VIA University College");
        UniversityName second = new UniversityName("VIA University College");
        Assert.Equal(first, second);
    }

    [Fact]
    public void UniversityName_WithDifferentValue_ShouldNotBeEqual()
    {
        UniversityName first = new UniversityName("VIA University College");
        UniversityName second = new UniversityName("Aarhus University");
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void UniversityName_ShouldStoreValue()
    {
        UniversityName universityName = new UniversityName("VIA University College");
        Assert.Equal("VIA University College", universityName.Value);
    }
}