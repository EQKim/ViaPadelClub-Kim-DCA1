namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Models;

public sealed class CourtReadModel
{
    public Guid Id { get; set; }
    public string CourtName { get; set; } = string.Empty;
}
