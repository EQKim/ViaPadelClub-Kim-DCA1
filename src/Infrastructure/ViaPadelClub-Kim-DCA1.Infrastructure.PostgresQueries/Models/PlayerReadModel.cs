namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Models;

public sealed class PlayerReadModel
{
    public Guid Id { get; set; }
    public string UniversityName { get; set; } = string.Empty;
    public bool IsVip { get; set; }
    public bool IsBanned { get; set; }
}
