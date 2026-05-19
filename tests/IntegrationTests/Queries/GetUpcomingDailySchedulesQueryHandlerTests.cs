using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.DailySchedules;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Handlers;

namespace IntegrationTests.Queries;

public sealed class GetUpcomingDailySchedulesQueryHandlerTests
{
    [NeonFact]
    public async Task HandleAsync_ShouldReturnUpcomingThreeDailySchedulesWithCourtsAndBookings()
    {
        await using QueryDbContext context = QueryDatabaseConnection.CreateContext();
        await context.Database.EnsureCreatedAsync();
        await QuerySeedData.ResetAssignment8DataAsync(context);

        DateTime baseDate = new(8000, 1, 1, 0, 0, 0);
        await QuerySeedData.SeedUpcomingSchedulesAsync(context, baseDate);

        GetUpcomingDailySchedulesQueryHandler handler = new(
            context,
            new FakeSystemTime(baseDate.AddHours(7)));

        UpcomingDailySchedulesAnswer answer =
            await handler.HandleAsync(new GetUpcomingDailySchedulesQuery());

        Assert.Equal(3, answer.DailySchedules.Count);
        Assert.All(answer.DailySchedules, schedule => Assert.NotEmpty(schedule.Courts));
        Assert.All(answer.DailySchedules, schedule => Assert.True(schedule.WindowEnd >= baseDate));
        Assert.Contains(answer.DailySchedules, schedule =>
            schedule.Courts.Any(court => court.ActiveBookings == 1 && court.Bookings.Count == 1));
    }
}
