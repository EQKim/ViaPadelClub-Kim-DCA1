using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateDailySchedule;

public sealed class CreateDailyScheduleCommand
{
    public DailyScheduleId DailyScheduleId { get; }
    public ManagerId ManagerId { get; }
    public TimeRange Window { get; }

    private CreateDailyScheduleCommand(
        DailyScheduleId dailyScheduleId,
        ManagerId managerId,
        TimeRange window)
    {
        DailyScheduleId = dailyScheduleId;
        ManagerId = managerId;
        Window = window;
    }

    public static Result<CreateDailyScheduleCommand> Create(
        Guid dailyScheduleId,
        Guid managerId,
        DateTime start,
        DateTime end)
    {
        if (dailyScheduleId == Guid.Empty)
        {
            return Result<CreateDailyScheduleCommand>.Failure(
                new Error("dailyschedule.id.empty", "Daily schedule id cannot be empty"));
        }

        if (managerId == Guid.Empty)
        {
            return Result<CreateDailyScheduleCommand>.Failure(
                new Error("manager.id.empty", "Manager id cannot be empty"));
        }

        if (end <= start)
        {
            return Result<CreateDailyScheduleCommand>.Failure(
                new Error("dailyschedule.invalid_timerange", "End time must be after start time"));
        }

        CreateDailyScheduleCommand command = new(
            new DailyScheduleId(dailyScheduleId),
            new ManagerId(managerId),
            new TimeRange(start, end));

        return Result<CreateDailyScheduleCommand>.Success(command);
    }
}