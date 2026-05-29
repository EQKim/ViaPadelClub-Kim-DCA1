using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.AddCourt;

public sealed class AddCourtCommand
{
    public DailyScheduleId DailyScheduleId { get; }
    public DailyScheduleCourtId DailyScheduleCourtId { get; }
    public CourtId CourtId { get; }
    public bool IsVipOnly { get; }

    private AddCourtCommand(
        DailyScheduleId dailyScheduleId,
        DailyScheduleCourtId dailyScheduleCourtId,
        CourtId courtId,
        bool isVipOnly)
    {
        DailyScheduleId = dailyScheduleId;
        DailyScheduleCourtId = dailyScheduleCourtId;
        CourtId = courtId;
        IsVipOnly = isVipOnly;
    }

    public static Result<AddCourtCommand> Create(
        Guid dailyScheduleId,
        Guid dailyScheduleCourtId,
        Guid courtId,
        bool isVipOnly)
    {
        if (dailyScheduleId == Guid.Empty)
            return Result<AddCourtCommand>.Failure(new Error("dailyschedule.id.empty", "Daily schedule id cannot be empty"));

        if (dailyScheduleCourtId == Guid.Empty)
            return Result<AddCourtCommand>.Failure(new Error("dailyschedulecourt.id.empty", "Daily schedule court id cannot be empty"));

        if (courtId == Guid.Empty)
            return Result<AddCourtCommand>.Failure(new Error("court.id.empty", "Court id cannot be empty"));

        return Result<AddCourtCommand>.Success(
            new AddCourtCommand(
                new DailyScheduleId(dailyScheduleId),
                new DailyScheduleCourtId(dailyScheduleCourtId),
                new CourtId(courtId),
                isVipOnly));
    }
}
