using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CancelBooking;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.DailySchedules.CancelBooking;

public sealed class CancelBookingCommandTests
{
    [Fact]
    public void Create_WithValidInput_ShouldCreateCommand()
    {
        Guid dailyScheduleId = Guid.NewGuid();
        Guid dailyScheduleCourtId = Guid.NewGuid();
        Guid bookingId = Guid.NewGuid();

        Result<CancelBookingCommand> result =
            CancelBookingCommand.Create(
                dailyScheduleId,
                dailyScheduleCourtId,
                bookingId);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(dailyScheduleId, result.Value.DailyScheduleId.Value);
        Assert.Equal(dailyScheduleCourtId, result.Value.DailyScheduleCourtId.Value);
        Assert.Equal(bookingId, result.Value.BookingId.Value);
    }

    [Fact]
    public void Create_WithEmptyBookingId_ShouldFail()
    {
        Result<CancelBookingCommand> result =
            CancelBookingCommand.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.Empty);

        Assert.True(result.IsFailure);
    }
}