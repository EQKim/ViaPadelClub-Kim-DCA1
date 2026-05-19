using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries;

namespace IntegrationTests.Queries;

public static class QueryDatabaseConnection
{
    public static QueryDbContext CreateContext()
    {
        DbContextOptions<QueryDbContext> options = new DbContextOptionsBuilder<QueryDbContext>()
            .UseNpgsql(DatabaseConnection.ConnectionString)
            .Options;

        return new QueryDbContext(options);
    }
}
