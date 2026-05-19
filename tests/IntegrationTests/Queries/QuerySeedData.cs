using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Models;

namespace IntegrationTests.Queries;

public static class QuerySeedData
{
    public static async Task ResetAssignment8DataAsync(QueryDbContext context)
    {
        await context.Database.ExecuteSqlRawAsync(
            "DELETE FROM daily_schedules WHERE window_start >= TIMESTAMP '8000-01-01 00:00:00'");

        await context.Database.ExecuteSqlRawAsync(
            "DELETE FROM players WHERE university_name LIKE {0}", "Assignment 8%");
    }

    public static async Task SeedUpcomingSchedulesAsync(QueryDbContext context, DateTime baseDate)
    {
        PlayerReadModel player = new()
        {
            Id = Guid.NewGuid(),
            UniversityName = "Assignment 8 VIA",
            IsVip = true,
            IsBanned = false
        };

        context.Players.Add(player);

        for (int index = 0; index < 4; index++)
        {
            DailyScheduleReadModel schedule = new()
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
                WindowStart = baseDate.AddDays(index).AddHours(8),
                WindowEnd = baseDate.AddDays(index).AddHours(16),
                Status = "Active"
            };

            DailyScheduleCourtReadModel court = new()
            {
                Id = Guid.NewGuid(),
                DailyScheduleId = schedule.Id,
                CourtId = Guid.NewGuid(),
                IsVipOnly = index % 2 == 0
            };

            BookingReadModel booking = new()
            {
                Id = Guid.NewGuid(),
                DailyScheduleCourtId = court.Id,
                PlayerId = player.Id,
                SlotStart = schedule.WindowStart.AddHours(1),
                SlotEnd = schedule.WindowStart.AddHours(2),
                Status = "Active"
            };

            context.DailySchedules.Add(schedule);
            context.DailyScheduleCourts.Add(court);
            context.Bookings.Add(booking);
        }

        await context.SaveChangesAsync();
    }

    public static async Task SeedPlayersAsync(QueryDbContext context)
    {
        context.Players.AddRange(
            new PlayerReadModel
            {
                Id = Guid.NewGuid(),
                UniversityName = "Assignment 8 VIA",
                IsVip = true,
                IsBanned = false
            },
            new PlayerReadModel
            {
                Id = Guid.NewGuid(),
                UniversityName = "Assignment 8 AU",
                IsVip = false,
                IsBanned = true
            });

        await context.SaveChangesAsync();
    }
}
