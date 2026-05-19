using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.DailySchedules;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.DailySchedules;

namespace PresentationTests;

public sealed class GetUpcomingDailySchedulesEndpointTests
{
    [Fact]
    public async Task Get_upcoming_daily_schedules_returns_mapped_response()
    {
        Guid scheduleId = Guid.NewGuid();
        using WebApplicationFactory<Program> factory = CreateFactory(scheduleId);
        HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/api/daily-schedules/upcoming?count=1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        GetUpcomingDailySchedulesResponse? body =
            await response.Content.ReadFromJsonAsync<GetUpcomingDailySchedulesResponse>();
        Assert.NotNull(body);
        Assert.Equal(scheduleId, body.DailySchedules.Single().DailyScheduleId);
    }

    private static WebApplicationFactory<Program> CreateFactory(Guid scheduleId)
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.RemoveAll<IQueryDispatcher>();
                services.AddScoped<IQueryDispatcher>(_ => new FakeQueryDispatcher(scheduleId));
            }));
    }

    private sealed class FakeQueryDispatcher : IQueryDispatcher
    {
        private readonly Guid _scheduleId;

        public FakeQueryDispatcher(Guid scheduleId)
        {
            _scheduleId = scheduleId;
        }

        public Task<TAnswer> DispatchAsync<TQuery, TAnswer>(TQuery query)
        {
            UpcomingDailySchedulesAnswer answer = new(new[]
            {
                new UpcomingDailyScheduleDto(
                    _scheduleId,
                    "Active",
                    new DateTime(2026, 5, 19, 10, 0, 0),
                    new DateTime(2026, 5, 19, 12, 0, 0),
                    Array.Empty<UpcomingDailyScheduleCourtDto>())
            });

            return Task.FromResult((TAnswer)(object)answer);
        }
    }
}
