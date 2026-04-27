using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CancelBooking;

public sealed class CancelBookingHandler : ICommandHandler<CancelBookingCommand>
{
    private readonly IDailyScheduleRepository _dailyScheduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelBookingHandler(
        IDailyScheduleRepository dailyScheduleRepository,
        IUnitOfWork unitOfWork)
    {
        _dailyScheduleRepository = dailyScheduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(CancelBookingCommand command)
    {
        DailySchedule? dailySchedule =
            await _dailyScheduleRepository.GetByIdAsync(command.DailyScheduleId);

        if (dailySchedule is null)
            return Result.Failure(
                new Error("dailyschedule.not_found", "Daily schedule was not found"));

        DailyScheduleCourt? court =
            dailySchedule.Courts.FirstOrDefault(c => c.Id == command.DailyScheduleCourtId);

        if (court is null)
            return Result.Failure(
                new Error("dailyschedulecourt.not_found", "Daily schedule court was not found"));

        Booking? booking =
            court.Bookings.FirstOrDefault(b => b.Id == command.BookingId);

        if (booking is null)
            return Result.Failure(
                new Error("booking.not_found", "Booking was not found"));

        Result result = booking.Cancel();

        if (result.IsFailure)
            return result;

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}