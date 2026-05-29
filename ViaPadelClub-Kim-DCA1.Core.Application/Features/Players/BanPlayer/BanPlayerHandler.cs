using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Managers;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.BanPlayer;

public sealed class BanPlayerHandler : ICommandHandler<BanPlayerCommand>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IDailyScheduleRepository _dailyScheduleRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BanPlayerHandler(
        IPlayerRepository playerRepository,
        IDailyScheduleRepository dailyScheduleRepository,
        IManagerRepository managerRepository,
        IUnitOfWork unitOfWork)
    {
        _playerRepository = playerRepository;
        _dailyScheduleRepository = dailyScheduleRepository;
        _managerRepository = managerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(BanPlayerCommand command)
    {
        Player? player = await _playerRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
            return Result.Failure(new Error("player.not_found", "Player was not found"));

        if (command.ManagerId.Value != Guid.Empty &&
            await _managerRepository.GetByIdAsync(command.ManagerId) is null)
        {
            return Result.Failure(new Error("manager.not_found", "Manager was not found"));
        }

        Result result = player.Ban(command.ManagerId, command.Reason);

        if (result.IsFailure)
            return result;

        DateTime now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
        IReadOnlyList<DailySchedule> dailySchedules =
            await _dailyScheduleRepository.GetSchedulesWithBookingsForPlayerAsync(player.Id, now);

        foreach (DailySchedule dailySchedule in dailySchedules)
        {
            dailySchedule.CancelActiveFutureBookingsForPlayer(player.Id, now);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
