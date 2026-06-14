using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;

public sealed class Booking : Entity<BookingId>
{
    public PlayerId PlayerId { get; private set; }
    public TimeRange Slot { get; private set; }
    public string Status { get; private set; }

    private Booking()
    {
        PlayerId = default!;
        Slot = default!;
        Status = default!;
    }

    public Booking(BookingId id, PlayerId playerId, TimeRange slot) : base(id)
    {
        PlayerId = playerId;
        Slot = slot;
        Status = "Active";
    }

    public Result Cancel()
    {
        if (Status == "Cancelled")
        {
            return Result.Failure(
                new Error("booking.already_cancelled", "Booking is already cancelled")
            );
        }

        Status = "Cancelled";
        return Result.Success();
    }
}
