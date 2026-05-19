namespace ViaPadelClub_Kim_DCA1.Core.Tools.ObjectMapper;

public interface IObjectMapper
{
    TDestination Map<TDestination>(object source);
}
