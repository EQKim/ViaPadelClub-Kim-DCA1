using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using Xunit;

namespace UnitTests.Domain.DailySchedules;

public class CreateBookingTests
{
    [Fact]
    public void CreateBooking_WithValidData_ShouldSucceed()
    {
        Player player = Player.Register(
            new PlayerId(Guid.NewGuid()),
            new UniversityName("VIA")).Value;

        DailySchedule schedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 8, 0, 0),
                new DateTime(2026, 1, 1, 16, 0, 0))).Value;

        schedule.Activate();

        DailyScheduleCourt court = schedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            false);

        Result<Booking> result = court.CreateBooking(
            player,
            new BookingId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 9, 0, 0),
                new DateTime(2026, 1, 1, 10, 0, 0)),
            schedule.Window);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void CreateBooking_ForBannedPlayer_ShouldFail()
    {
        Player player = Player.Register(
            new PlayerId(Guid.NewGuid()),
            new UniversityName("VIA")).Value;
        player.Ban();

        DailySchedule schedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 8, 0, 0),
                new DateTime(2026, 1, 1, 16, 0, 0))).Value;

        schedule.Activate();

        DailyScheduleCourt court = schedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            false);

        Result<Booking> result = court.CreateBooking(
            player,
            new BookingId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 9, 0, 0),
                new DateTime(2026, 1, 1, 10, 0, 0)),
            schedule.Window);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void CreateBooking_OutsideDailyWindow_ShouldFail()
    {
        Player player = Player.Register(
            new PlayerId(Guid.NewGuid()),
            new UniversityName("VIA")).Value;

        DailySchedule schedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 8, 0, 0),
                new DateTime(2026, 1, 1, 16, 0, 0))).Value;

        schedule.Activate();

        DailyScheduleCourt court = schedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            false);

        Result<Booking> result = court.CreateBooking(
            player,
            new BookingId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 17, 0, 0),
                new DateTime(2026, 1, 1, 18, 0, 0)),
            schedule.Window);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void CreateBooking_WithOverlappingBooking_ShouldFail()
    {
        Player player = Player.Register(
            new PlayerId(Guid.NewGuid()),
            new UniversityName("VIA")).Value;

        DailySchedule schedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 8, 0, 0),
                new DateTime(2026, 1, 1, 16, 0, 0))).Value;

        schedule.Activate();

        DailyScheduleCourt court = schedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            false);

        court.CreateBooking(
            player,
            new BookingId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 9, 0, 0),
                new DateTime(2026, 1, 1, 10, 0, 0)),
            schedule.Window);

        Result<Booking> result = court.CreateBooking(
            player,
            new BookingId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 9, 30, 0),
                new DateTime(2026, 1, 1, 10, 30, 0)),
            schedule.Window);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void CreateBooking_OnVipOnlyCourt_WithNonVipPlayer_ShouldFail()
    {
        Player player = Player.Register(
            new PlayerId(Guid.NewGuid()),
            new UniversityName("VIA")).Value;

        DailySchedule schedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 8, 0, 0),
                new DateTime(2026, 1, 1, 16, 0, 0))).Value;

        schedule.Activate();

        DailyScheduleCourt court = schedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            true);

        Result<Booking> result = court.CreateBooking(
            player,
            new BookingId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 9, 0, 0),
                new DateTime(2026, 1, 1, 10, 0, 0)),
            schedule.Window);

        Assert.True(result.IsFailure);
    }
}