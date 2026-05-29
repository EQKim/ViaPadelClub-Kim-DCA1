using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.AddCourt;

public sealed class AddCourtHandler : ICommandHandler<AddCourtCommand>
{
    private readonly IDailyScheduleRepository _dailyScheduleRepository;
    private readonly ICourtRepository _courtRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCourtHandler(
        IDailyScheduleRepository dailyScheduleRepository,
        ICourtRepository courtRepository,
        IUnitOfWork unitOfWork)
    {
        _dailyScheduleRepository = dailyScheduleRepository;
        _courtRepository = courtRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(AddCourtCommand command)
    {
        DailySchedule? dailySchedule = await _dailyScheduleRepository.GetByIdAsync(command.DailyScheduleId);

        if (dailySchedule is null)
            return Result.Failure(new Error("dailyschedule.not_found", "Daily schedule was not found"));

        Court? court = await _courtRepository.GetByIdAsync(command.CourtId);

        if (court is null)
            return Result.Failure(new Error("court.not_found", "Court was not found"));

        dailySchedule.AddCourt(command.DailyScheduleCourtId, command.CourtId, command.IsVipOnly);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
