using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateDailySchedule;

public sealed class CreateDailyScheduleHandler : ICommandHandler<CreateDailyScheduleCommand>
{
    private readonly IDailyScheduleRepository _dailyScheduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDailyScheduleHandler(
        IDailyScheduleRepository dailyScheduleRepository,
        IUnitOfWork unitOfWork)
    {
        _dailyScheduleRepository = dailyScheduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(CreateDailyScheduleCommand command)
    {
        Result<DailySchedule> result = DailySchedule.Create(
            command.DailyScheduleId,
            command.ManagerId,
            command.Window);

        if (result.IsFailure)
        {
            return Result.Failure(result.Errors);
        }

        await _dailyScheduleRepository.AddAsync(result.Value!);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}