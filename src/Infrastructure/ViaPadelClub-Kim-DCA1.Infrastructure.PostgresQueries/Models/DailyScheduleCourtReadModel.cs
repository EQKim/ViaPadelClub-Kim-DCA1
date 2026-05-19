namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Models;

public sealed class DailyScheduleCourtReadModel
{
    public Guid Id { get; set; }
    public Guid DailyScheduleId { get; set; }
    public Guid CourtId { get; set; }
    public bool IsVipOnly { get; set; }

    public DailyScheduleReadModel DailySchedule { get; set; } = default!;
    public List<BookingReadModel> Bookings { get; set; } = new();
}
