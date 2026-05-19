using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Repositories;

namespace IntegrationTests.Repositories;

public sealed class DailyScheduleRepositoryTests
{
    [NeonFact]
    public async Task AddAndGetByIdAsync_ShouldPersistScheduleCourtsAndBookings()
    {
        await using DmContext context = DatabaseConnection.CreateContext();
        await context.Database.EnsureCreatedAsync();

        DailySchedule dailySchedule = DailySchedule.Create(
            new DailyScheduleId(Guid.NewGuid()),
            new ManagerId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 8, 0, 0),
                new DateTime(2026, 1, 1, 16, 0, 0))).Value!;

        dailySchedule.Activate();

        DailyScheduleCourt court = dailySchedule.AddCourt(
            new DailyScheduleCourtId(Guid.NewGuid()),
            new CourtId(Guid.NewGuid()),
            true);

        Player player = Player.Register(
            new PlayerId(Guid.NewGuid()),
            new UniversityName("VIA University College")).Value!;
        player.GrantVip();

        court.CreateBooking(
            player,
            new BookingId(Guid.NewGuid()),
            new TimeRange(
                new DateTime(2026, 1, 1, 9, 0, 0),
                new DateTime(2026, 1, 1, 10, 0, 0)),
            dailySchedule.Window);

        DailyScheduleRepository repository = new(context);
        UnitOfWork unitOfWork = new(context);

        await repository.AddAsync(dailySchedule);
        await unitOfWork.SaveChangesAsync();

        context.ChangeTracker.Clear();

        DailySchedule? loaded = await repository.GetByIdAsync(dailySchedule.Id);

        Assert.NotNull(loaded);
        Assert.Equal("Active", loaded.Status);
        Assert.Equal(dailySchedule.Window, loaded.Window);

        DailyScheduleCourt loadedCourt = Assert.Single(loaded.Courts);
        Assert.True(loadedCourt.IsVipOnly);

        Booking loadedBooking = Assert.Single(loadedCourt.Bookings);
        Assert.Equal(player.Id, loadedBooking.PlayerId);
        Assert.Equal("Active", loadedBooking.Status);
    }
}
