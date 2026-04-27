using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.ActivateDailySchedule;

public sealed class ActivateDailyScheduleHandler : ICommandHandler<ActivateDailyScheduleCommand>
{
    private readonly IDailyScheduleRepository _dailyScheduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateDailyScheduleHandler(
        IDailyScheduleRepository dailyScheduleRepository,
        IUnitOfWork unitOfWork)
    {
        _dailyScheduleRepository = dailyScheduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(ActivateDailyScheduleCommand command)
    {
        DailySchedule? dailySchedule =
            await _dailyScheduleRepository.GetByIdAsync(command.DailyScheduleId);

        if (dailySchedule is null)
        {
            return Result.Failure(
                new Error("dailyschedule.not_found", "Daily schedule was not found"));
        }

        Result result = dailySchedule.Activate();

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}