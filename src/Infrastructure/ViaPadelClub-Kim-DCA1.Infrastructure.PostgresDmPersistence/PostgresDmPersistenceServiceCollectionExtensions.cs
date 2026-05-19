using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Repositories;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence;

public static class PostgresDmPersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresDmPersistence(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<DmContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IDailyScheduleRepository, DailyScheduleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
