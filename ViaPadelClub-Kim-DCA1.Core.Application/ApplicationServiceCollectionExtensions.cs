using Microsoft.Extensions.DependencyInjection;
using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Courts.CreateCourt;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.ActivateDailySchedule;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.AddCourt;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CancelBooking;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateBooking;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.DailySchedules.CreateDailySchedule;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Managers.CreateManager;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.BanPlayer;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.GrantVip;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RegisterPlayer;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RevokeVip;
using ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.UnbanPlayer;

namespace ViaPadelClub_Kim_DCA1.Core.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationCommands(this IServiceCollection services)
    {
        services.AddTransient<ICommandHandler<RegisterPlayerCommand>, RegisterPlayerHandler>();
        services.AddTransient<ICommandHandler<CreateManagerCommand>, CreateManagerHandler>();
        services.AddTransient<ICommandHandler<CreateCourtCommand>, CreateCourtHandler>();
        services.AddTransient<ICommandHandler<BanPlayerCommand>, BanPlayerHandler>();
        services.AddTransient<ICommandHandler<GrantVipCommand>, GrantVipHandler>();
        services.AddTransient<ICommandHandler<RevokeVipCommand>, RevokeVipHandler>();
        services.AddTransient<ICommandHandler<UnbanPlayerCommand>, UnbanPlayerHandler>();
        services.AddTransient<ICommandHandler<CreateDailyScheduleCommand>, CreateDailyScheduleHandler>();
        services.AddTransient<ICommandHandler<AddCourtCommand>, AddCourtHandler>();
        services.AddTransient<ICommandHandler<ActivateDailyScheduleCommand>, ActivateDailyScheduleHandler>();
        services.AddTransient<ICommandHandler<CreateBookingCommand>, CreateBookingHandler>();
        services.AddTransient<ICommandHandler<CancelBookingCommand>, CancelBookingHandler>();

        services.AddScoped<ICommandDispatcher>(provider => new CommandDispatcher()
            .Register(provider.GetRequiredService<ICommandHandler<RegisterPlayerCommand>>())
            .Register(provider.GetRequiredService<ICommandHandler<CreateManagerCommand>>())
            .Register(provider.GetRequiredService<ICommandHandler<CreateCourtCommand>>())
            .Register(provider.GetRequiredService<ICommandHandler<BanPlayerCommand>>())
            .Register(provider.GetRequiredService<ICommandHandler<GrantVipCommand>>())
            .Register(provider.GetRequiredService<ICommandHandler<RevokeVipCommand>>())
            .Register(provider.GetRequiredService<ICommandHandler<UnbanPlayerCommand>>())
            .Register(provider.GetRequiredService<ICommandHandler<CreateDailyScheduleCommand>>())
            .Register(provider.GetRequiredService<ICommandHandler<AddCourtCommand>>())
            .Register(provider.GetRequiredService<ICommandHandler<ActivateDailyScheduleCommand>>())
            .Register(provider.GetRequiredService<ICommandHandler<CreateBookingCommand>>())
            .Register(provider.GetRequiredService<ICommandHandler<CancelBookingCommand>>()));

        return services;
    }
}
