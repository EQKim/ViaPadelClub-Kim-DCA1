using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.Players;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Time;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Handlers;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Time;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries;

public static class PostgresQueriesServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresQueries(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<QueryDbContext>(options => options.UseNpgsql(connectionString));
        services.AddSingleton<ISystemTime, SystemTime>();
        services.AddScoped<IQueryHandler<GetUpcomingDailySchedulesQuery, UpcomingDailySchedulesAnswer>, GetUpcomingDailySchedulesQueryHandler>();
        services.AddScoped<IQueryHandler<GetPlayerDirectoryQuery, PlayerDirectoryAnswer>, GetPlayerDirectoryQueryHandler>();

        services.AddScoped<IQueryDispatcher>(provider => new QueryDispatcher()
            .Register(provider.GetRequiredService<IQueryHandler<GetUpcomingDailySchedulesQuery, UpcomingDailySchedulesAnswer>>())
            .Register(provider.GetRequiredService<IQueryHandler<GetPlayerDirectoryQuery, PlayerDirectoryAnswer>>()));

        return services;
    }
}
