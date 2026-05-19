using System.Web;
using Npgsql;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Configuration;

public static class PostgresConnectionString
{
    public static string Normalize(string connectionString)
    {
        if (!Uri.TryCreate(connectionString, UriKind.Absolute, out Uri? uri)
            || (uri.Scheme != "postgres" && uri.Scheme != "postgresql"))
        {
            return connectionString;
        }

        string[] userInfo = uri.UserInfo.Split(':', 2);
        string username = Uri.UnescapeDataString(userInfo.ElementAtOrDefault(0) ?? string.Empty);
        string password = Uri.UnescapeDataString(userInfo.ElementAtOrDefault(1) ?? string.Empty);
        string database = Uri.UnescapeDataString(uri.AbsolutePath.TrimStart('/'));
        string sslMode = HttpUtility.ParseQueryString(uri.Query)["sslmode"] ?? "require";

        NpgsqlConnectionStringBuilder builder = new()
        {
            Host = uri.Host,
            Port = uri.Port > 0 ? uri.Port : 5432,
            Database = database,
            Username = username,
            Password = password,
            SslMode = sslMode.Equals("disable", StringComparison.OrdinalIgnoreCase)
                ? SslMode.Disable
                : SslMode.Require
        };

        return builder.ConnectionString;
    }
}
