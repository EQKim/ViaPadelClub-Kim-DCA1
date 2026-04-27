using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.ActivateDailySchedule;

public sealed class ActivateDailyScheduleCommand
{
    public DailyScheduleId DailyScheduleId { get; }

    private ActivateDailyScheduleCommand(DailyScheduleId dailyScheduleId)
    {
        DailyScheduleId = dailyScheduleId;
    }

    public static Result<ActivateDailyScheduleCommand> Create(Guid dailyScheduleId)
    {
        if (dailyScheduleId == Guid.Empty)
        {
            return Result<ActivateDailyScheduleCommand>.Failure(
                new Error("dailyschedule.id.empty", "Daily schedule id cannot be empty"));
        }

        return Result<ActivateDailyScheduleCommand>.Success(
            new ActivateDailyScheduleCommand(new DailyScheduleId(dailyScheduleId)));
    }
}