namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Models;

public sealed class DailyScheduleReadModel
{
    public Guid Id { get; set; }
    public Guid ManagerId { get; set; }
    public DateTime WindowStart { get; set; }
    public DateTime WindowEnd { get; set; }
    public string Status { get; set; } = string.Empty;

    public List<DailyScheduleCourtReadModel> Courts { get; set; } = new();
}
