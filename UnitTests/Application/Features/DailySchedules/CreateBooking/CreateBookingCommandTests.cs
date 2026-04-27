using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateBooking;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Features.DailySchedules.CreateBooking;

public sealed class CreateBookingCommandTests
{
    [Fact]
    public void Create_WithValidInput_ShouldCreateCommand()
    {
        DateTime start = DateTime.Today.AddHours(10);
        DateTime end = DateTime.Today.AddHours(11);

        Result<CreateBookingCommand> result =
            CreateBookingCommand.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                start,
                end);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(start, result.Value.Slot.Start);
        Assert.Equal(end, result.Value.Slot.End);
    }

    [Fact]
    public void Create_WithInvalidTimeRange_ShouldFail()
    {
        DateTime start = DateTime.Today.AddHours(11);
        DateTime end = DateTime.Today.AddHours(10);

        Result<CreateBookingCommand> result =
            CreateBookingCommand.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                start,
                end);

        Assert.True(result.IsFailure);
    }
}