namespace ViaPadelClub_Kim_DCA1.Core.Tools.ObjectMapper;

public interface IMapping<in TSource, out TDestination>
{
    TDestination Map(TSource source);
}
