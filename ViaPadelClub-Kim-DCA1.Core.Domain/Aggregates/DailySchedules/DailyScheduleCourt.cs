using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;

public sealed class DailyScheduleCourt : Entity<DailyScheduleCourtId>
{
    private readonly List<Booking> _bookings = new();

    public CourtId CourtId { get; private set; }
    public bool IsVipOnly { get; private set; }
    public IReadOnlyList<Booking> Bookings => _bookings;

    private DailyScheduleCourt()
    {
        CourtId = default!;
    }

    public DailyScheduleCourt(DailyScheduleCourtId id, CourtId courtId, bool isVipOnly) : base(id)
    {
        CourtId = courtId;
        IsVipOnly = isVipOnly;
    }

    public Result<Booking> CreateBooking(Player player, BookingId bookingId, TimeRange slot, TimeRange dailyWindow)
    {
        if (player.IsBanned)
        {
            return Result<Booking>.Failure(
                new Error("booking.player_banned", "Banned player cannot create booking")
            );
        }

        if (IsVipOnly && !player.IsVip)
        {
            return Result<Booking>.Failure(
                new Error("booking.vip_required", "VIP status is required")
            );
        }

        if (slot.Start < dailyWindow.Start || slot.End > dailyWindow.End || slot.End <= slot.Start)
        {
            return Result<Booking>.Failure(
                new Error("booking.invalid_time", "Booking must be within daily schedule window")
            );
        }

        bool overlaps = _bookings.Any(b =>
            b.Status == "Active" &&
            slot.Start < b.Slot.End &&
            slot.End > b.Slot.Start);

        if (overlaps)
        {
            return Result<Booking>.Failure(
                new Error("booking.overlap", "Booking overlaps an existing booking")
            );
        }

        Booking booking = new Booking(bookingId, player.Id, slot);
        _bookings.Add(booking);

        return Result<Booking>.Success(booking);
    }
}
