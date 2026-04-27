using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateBooking;

public sealed class CreateBookingHandler : ICommandHandler<CreateBookingCommand>
{
    private readonly IDailyScheduleRepository _dailyScheduleRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookingHandler(
        IDailyScheduleRepository dailyScheduleRepository,
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork)
    {
        _dailyScheduleRepository = dailyScheduleRepository;
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(CreateBookingCommand command)
    {
        DailySchedule? dailySchedule =
            await _dailyScheduleRepository.GetByIdAsync(command.DailyScheduleId);

        if (dailySchedule is null)
            return Result.Failure(new Error("dailyschedule.not_found", "Daily schedule was not found"));

        Player? player =
            await _playerRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
            return Result.Failure(new Error("player.not_found", "Player was not found"));

        DailyScheduleCourt? court =
            dailySchedule.Courts.FirstOrDefault(c => c.Id == command.DailyScheduleCourtId);

        if (court is null)
            return Result.Failure(new Error("dailyschedulecourt.not_found", "Daily schedule court was not found"));

        Result<Booking> result =
            court.CreateBooking(player, command.BookingId, command.Slot, dailySchedule.Window);

        if (result.IsFailure)
            return Result.Failure(result.Errors);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}