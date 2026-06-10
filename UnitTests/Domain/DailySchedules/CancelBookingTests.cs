using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using Xunit;

namespace UnitTests.Domain.DailySchedules;

public class CancelBookingTests
{
    [Fact]
    public void Cancel_OnActiveBooking_ShouldSucceed()
    {
        Player player = Player.Register(
            new PlayerId(Guid.NewGuid()),
            new UniversityName("VIA")).Value!;

        DailySchedule schedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 8, 0, 0),
                new DateTime(2026, 1, 1, 16, 0, 0))).Value!;

        schedule.Activate();

        DailyScheduleCourt court = schedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            false);

        Booking booking = court.CreateBooking(
            player,
            new BookingId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 9, 0, 0),
                new DateTime(2026, 1, 1, 10, 0, 0)),
            schedule.Window).Value!;

        Result result = booking.Cancel();

        Assert.True(result.IsSuccess);
        Assert.Equal("Cancelled", booking.Status);
    }

    [Fact]
    public void Cancel_OnAlreadyCancelledBooking_ShouldFail()
    {
        Player player = Player.Register(
            new PlayerId(Guid.NewGuid()),
            new UniversityName("VIA")).Value!;

        DailySchedule schedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 8, 0, 0),
                new DateTime(2026, 1, 1, 16, 0, 0))).Value!;

        schedule.Activate();

        DailyScheduleCourt court = schedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            false);

        Booking booking = court.CreateBooking(
            player,
            new BookingId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 9, 0, 0),
                new DateTime(2026, 1, 1, 10, 0, 0)),
            schedule.Window).Value!;

        booking.Cancel();
        Result result = booking.Cancel();

        Assert.True(result.IsFailure);
    }
}