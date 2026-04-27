using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CancelBooking;

public sealed class CancelBookingCommand
{
    public DailyScheduleId DailyScheduleId { get; }
    public DailyScheduleCourtId DailyScheduleCourtId { get; }
    public BookingId BookingId { get; }

    private CancelBookingCommand(
        DailyScheduleId dailyScheduleId,
        DailyScheduleCourtId dailyScheduleCourtId,
        BookingId bookingId)
    {
        DailyScheduleId = dailyScheduleId;
        DailyScheduleCourtId = dailyScheduleCourtId;
        BookingId = bookingId;
    }

    public static Result<CancelBookingCommand> Create(
        Guid dailyScheduleId,
        Guid dailyScheduleCourtId,
        Guid bookingId)
    {
        if (dailyScheduleId == Guid.Empty)
            return Result<CancelBookingCommand>.Failure(
                new Error("dailyschedule.id.empty", "Daily schedule id cannot be empty"));

        if (dailyScheduleCourtId == Guid.Empty)
            return Result<CancelBookingCommand>.Failure(
                new Error("dailyschedulecourt.id.empty", "Daily schedule court id cannot be empty"));

        if (bookingId == Guid.Empty)
            return Result<CancelBookingCommand>.Failure(
                new Error("booking.id.empty", "Booking id cannot be empty"));

        return Result<CancelBookingCommand>.Success(
            new CancelBookingCommand(
                new DailyScheduleId(dailyScheduleId),
                new DailyScheduleCourtId(dailyScheduleCourtId),
                new BookingId(bookingId)));
    }
}