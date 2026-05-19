namespace IntegrationTests;

public sealed class NeonFactAttribute : FactAttribute
{
    public NeonFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(DatabaseConnection.ConnectionString))
        {
            Skip = "Set VIAPADELCLUB_POSTGRES_CONNECTION_STRING to run Neon PostgreSQL integration tests.";
        }
    }
}
