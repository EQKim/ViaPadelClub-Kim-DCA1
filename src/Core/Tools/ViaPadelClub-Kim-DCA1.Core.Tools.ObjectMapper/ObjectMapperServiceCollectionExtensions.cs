using Microsoft.Extensions.DependencyInjection;

namespace ViaPadelClub_Kim_DCA1.Core.Tools.ObjectMapper;

public static class ObjectMapperServiceCollectionExtensions
{
    public static IServiceCollection AddObjectMapper(this IServiceCollection services)
    {
        services.AddSingleton<ObjectMapper>();
        services.AddSingleton<IObjectMapper>(provider => provider.GetRequiredService<ObjectMapper>());

        return services;
    }
}
