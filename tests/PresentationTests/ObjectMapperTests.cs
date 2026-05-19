using ViaPadelClub_Kim_DCA1.Core.Tools.ObjectMapper;

namespace PresentationTests;

public sealed class ObjectMapperTests
{
    [Fact]
    public void Map_uses_matching_constructor_properties()
    {
        ObjectMapper mapper = new();

        Destination result = mapper.Map<Destination>(new Source("Kim", 7));

        Assert.Equal("Kim", result.Name);
        Assert.Equal(7, result.Number);
    }

    [Fact]
    public void Map_uses_registered_mapping_when_available()
    {
        ObjectMapper mapper = new();
        mapper.Register(new CustomMapping());

        Destination result = mapper.Map<Destination>(new Source("Kim", 7));

        Assert.Equal("KIM", result.Name);
        Assert.Equal(14, result.Number);
    }

    private sealed record Source(string Name, int Number);

    private sealed record Destination(string Name, int Number);

    private sealed class CustomMapping : IMapping<Source, Destination>
    {
        public Destination Map(Source source)
        {
            return new Destination(source.Name.ToUpperInvariant(), source.Number * 2);
        }
    }
}
