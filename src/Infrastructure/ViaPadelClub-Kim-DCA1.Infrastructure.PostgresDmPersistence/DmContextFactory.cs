using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence;

public sealed class DmContextFactory : IDesignTimeDbContextFactory<DmContext>
{
    public DmContext CreateDbContext(string[] args)
    {
        string? connectionString =
            Environment.GetEnvironmentVariable("VIAPADELCLUB_POSTGRES_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Set VIAPADELCLUB_POSTGRES_CONNECTION_STRING before using EF design-time tools.");
        }

        DbContextOptionsBuilder<DmContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(connectionString);

        return new DmContext(optionsBuilder.Options);
    }
}
