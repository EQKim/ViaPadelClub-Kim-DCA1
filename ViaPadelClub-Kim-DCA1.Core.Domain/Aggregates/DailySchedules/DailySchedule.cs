using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;

public sealed class DailySchedule : AggregateRoot<DailyScheduleId>
{
    private readonly List<DailyScheduleCourt> _courts = new();

    public ManagerId ManagerId { get; private set; }
    public TimeRange Window { get; private set; }
    public string Status { get; private set; }
    public IReadOnlyList<DailyScheduleCourt> Courts => _courts;

    private DailySchedule(DailyScheduleId id, ManagerId managerId, TimeRange window) : base(id)
    {
        ManagerId = managerId;
        Window = window;
        Status = "Draft";
    }

    public static Result<DailySchedule> Create(DailyScheduleId id, ManagerId managerId, TimeRange window)
    {
        if (window.End <= window.Start)
        {
            return Result<DailySchedule>.Failure(
                new Error("dailyschedule.invalid_timerange", "End time must be after start time")
            );
        }

        DailySchedule dailySchedule = new DailySchedule(id, managerId, window);
        return Result<DailySchedule>.Success(dailySchedule);
    }

    public Result Activate()
    {
        if (Status == "Active")
        {
            return Result.Failure(
                new Error("dailyschedule.already_active", "Daily schedule is already active")
            );
        }

        Status = "Active";
        return Result.Success();
    }

    public DailyScheduleCourt AddCourt(DailyScheduleCourtId id, CourtId courtId, bool isVipOnly)
    {
        DailyScheduleCourt court = new DailyScheduleCourt(id, courtId, isVipOnly);
        _courts.Add(court);
        return court;
    }
}