using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence;

namespace IntegrationTests;

public static class DatabaseConnection
{
    public static string? ConnectionString =>
        Environment.GetEnvironmentVariable("VIAPADELCLUB_POSTGRES_CONNECTION_STRING");

    public static DmContext CreateContext()
    {
        DbContextOptions<DmContext> options = new DbContextOptionsBuilder<DmContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        return new DmContext(options);
    }
}
