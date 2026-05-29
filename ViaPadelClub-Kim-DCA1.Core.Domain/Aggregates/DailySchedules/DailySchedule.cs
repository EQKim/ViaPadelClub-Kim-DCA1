using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
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

    private DailySchedule()
    {
        ManagerId = default!;
        Window = default!;
        Status = default!;
    }

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

    public Result<Booking> CreateBooking(
        Player player,
        DailyScheduleCourtId dailyScheduleCourtId,
        BookingId bookingId,
        TimeRange slot)
    {
        if (Status != "Active")
        {
            return Result<Booking>.Failure(
                new Error("dailyschedule.not_active", "Daily schedule must be active to allow booking")
            );
        }

        DailyScheduleCourt? court = _courts.FirstOrDefault(c => c.Id == dailyScheduleCourtId);

        if (court is null)
        {
            return Result<Booking>.Failure(
                new Error("dailyschedulecourt.not_found", "Daily schedule court was not found")
            );
        }

        return court.CreateBooking(player, bookingId, slot, Window);
    }
    public int CancelActiveFutureBookingsForPlayer(PlayerId playerId, DateTime from)
    {
        int cancelledBookings = 0;

        foreach (Booking booking in _courts.SelectMany(court => court.Bookings))
        {
            if (booking.PlayerId != playerId ||
                booking.Status != "Active" ||
                booking.Slot.Start < from)
            {
                continue;
            }

            Result result = booking.Cancel();

            if (result.IsSuccess)
                cancelledBookings++;
        }

        return cancelledBookings;
    }
}
