using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Models;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries;

public sealed class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options)
    {
    }

    public DbSet<PlayerReadModel> Players => Set<PlayerReadModel>();
    public DbSet<DailyScheduleReadModel> DailySchedules => Set<DailyScheduleReadModel>();
    public DbSet<DailyScheduleCourtReadModel> DailyScheduleCourts => Set<DailyScheduleCourtReadModel>();
    public DbSet<BookingReadModel> Bookings => Set<BookingReadModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerReadModel>(builder =>
        {
            builder.ToTable("players");
            builder.HasKey(player => player.Id);
            builder.Property(player => player.Id).HasColumnName("id");
            builder.Property(player => player.UniversityName).HasColumnName("university_name");
            builder.Property(player => player.IsVip).HasColumnName("is_vip");
            builder.Property(player => player.IsBanned).HasColumnName("is_banned");
        });

        modelBuilder.Entity<DailyScheduleReadModel>(builder =>
        {
            builder.ToTable("daily_schedules");
            builder.HasKey(schedule => schedule.Id);
            builder.Property(schedule => schedule.Id).HasColumnName("id");
            builder.Property(schedule => schedule.ManagerId).HasColumnName("manager_id");
            builder.Property(schedule => schedule.WindowStart)
                .HasColumnName("window_start")
                .HasColumnType("timestamp without time zone");
            builder.Property(schedule => schedule.WindowEnd)
                .HasColumnName("window_end")
                .HasColumnType("timestamp without time zone");
            builder.Property(schedule => schedule.Status).HasColumnName("status");

            builder.HasMany(schedule => schedule.Courts)
                .WithOne(court => court.DailySchedule)
                .HasForeignKey(court => court.DailyScheduleId);
        });

        modelBuilder.Entity<DailyScheduleCourtReadModel>(builder =>
        {
            builder.ToTable("daily_schedule_courts");
            builder.HasKey(court => court.Id);
            builder.Property(court => court.Id).HasColumnName("id");
            builder.Property(court => court.DailyScheduleId).HasColumnName("daily_schedule_id");
            builder.Property(court => court.CourtId).HasColumnName("court_id");
            builder.Property(court => court.IsVipOnly).HasColumnName("is_vip_only");

            builder.HasMany(court => court.Bookings)
                .WithOne(booking => booking.DailyScheduleCourt)
                .HasForeignKey(booking => booking.DailyScheduleCourtId);
        });

        modelBuilder.Entity<BookingReadModel>(builder =>
        {
            builder.ToTable("bookings");
            builder.HasKey(booking => booking.Id);
            builder.Property(booking => booking.Id).HasColumnName("id");
            builder.Property(booking => booking.DailyScheduleCourtId).HasColumnName("daily_schedule_court_id");
            builder.Property(booking => booking.PlayerId).HasColumnName("player_id");
            builder.Property(booking => booking.SlotStart)
                .HasColumnName("slot_start")
                .HasColumnType("timestamp without time zone");
            builder.Property(booking => booking.SlotEnd)
                .HasColumnName("slot_end")
                .HasColumnType("timestamp without time zone");
            builder.Property(booking => booking.Status).HasColumnName("status");
        });
    }
}
