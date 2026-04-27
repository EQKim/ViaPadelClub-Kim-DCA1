using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateBooking;

public sealed class CreateBookingCommand
{
    public DailyScheduleId DailyScheduleId { get; }
    public DailyScheduleCourtId DailyScheduleCourtId { get; }
    public BookingId BookingId { get; }
    public PlayerId PlayerId { get; }
    public TimeRange Slot { get; }

    private CreateBookingCommand(
        DailyScheduleId dailyScheduleId,
        DailyScheduleCourtId dailyScheduleCourtId,
        BookingId bookingId,
        PlayerId playerId,
        TimeRange slot)
    {
        DailyScheduleId = dailyScheduleId;
        DailyScheduleCourtId = dailyScheduleCourtId;
        BookingId = bookingId;
        PlayerId = playerId;
        Slot = slot;
    }

    public static Result<CreateBookingCommand> Create(
        Guid dailyScheduleId,
        Guid dailyScheduleCourtId,
        Guid bookingId,
        Guid playerId,
        DateTime start,
        DateTime end)
    {
        if (dailyScheduleId == Guid.Empty)
            return Result<CreateBookingCommand>.Failure(new Error("dailyschedule.id.empty", "Daily schedule id cannot be empty"));

        if (dailyScheduleCourtId == Guid.Empty)
            return Result<CreateBookingCommand>.Failure(new Error("dailyschedulecourt.id.empty", "Daily schedule court id cannot be empty"));

        if (bookingId == Guid.Empty)
            return Result<CreateBookingCommand>.Failure(new Error("booking.id.empty", "Booking id cannot be empty"));

        if (playerId == Guid.Empty)
            return Result<CreateBookingCommand>.Failure(new Error("player.id.empty", "Player id cannot be empty"));

        if (end <= start)
            return Result<CreateBookingCommand>.Failure(new Error("booking.invalid_time", "End time must be after start time"));

        return Result<CreateBookingCommand>.Success(
            new CreateBookingCommand(
                new DailyScheduleId(dailyScheduleId),
                new DailyScheduleCourtId(dailyScheduleCourtId),
                new BookingId(bookingId),
                new PlayerId(playerId),
                new TimeRange(start, end)));
    }
}