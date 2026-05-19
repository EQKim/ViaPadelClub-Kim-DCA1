using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries;

public sealed class QueryDbContextFactory : IDesignTimeDbContextFactory<QueryDbContext>
{
    public QueryDbContext CreateDbContext(string[] args)
    {
        string? connectionString =
            Environment.GetEnvironmentVariable("VIAPADELCLUB_POSTGRES_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Set VIAPADELCLUB_POSTGRES_CONNECTION_STRING before using EF design-time tools.");
        }

        DbContextOptionsBuilder<QueryDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(connectionString);

        return new QueryDbContext(optionsBuilder.Options);
    }
}
