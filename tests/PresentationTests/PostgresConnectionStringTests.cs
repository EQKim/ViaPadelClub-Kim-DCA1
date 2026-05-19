using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Configuration;

namespace PresentationTests;

public sealed class PostgresConnectionStringTests
{
    [Fact]
    public void Normalize_converts_neon_uri_to_npgsql_connection_string()
    {
        string result = PostgresConnectionString.Normalize(
            "postgresql://user:pass@example.neon.tech/neondb?sslmode=require");

        Assert.Contains("Host=example.neon.tech", result);
        Assert.Contains("Database=neondb", result);
        Assert.Contains("Username=user", result);
        Assert.Contains("Password=pass", result);
        Assert.Contains("SSL Mode=Require", result);
    }

    [Fact]
    public void Normalize_keeps_existing_npgsql_connection_string()
    {
        const string connectionString = "Host=localhost;Database=test;Username=user;Password=pass";

        string result = PostgresConnectionString.Normalize(connectionString);

        Assert.Equal(connectionString, result);
    }
}
