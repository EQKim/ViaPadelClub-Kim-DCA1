namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Models;

public sealed class BookingReadModel
{
    public Guid Id { get; set; }
    public Guid DailyScheduleCourtId { get; set; }
    public Guid PlayerId { get; set; }
    public DateTime SlotStart { get; set; }
    public DateTime SlotEnd { get; set; }
    public string Status { get; set; } = string.Empty;

    public DailyScheduleCourtReadModel DailyScheduleCourt { get; set; } = default!;
}
