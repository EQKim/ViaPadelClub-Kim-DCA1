using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using ViaPadelClub_Kim_DCA1.Presentation.WebApi.Endpoints.Players;

namespace PresentationTests;

public sealed class RegisterPlayerEndpointTests
{
    [Fact]
    public async Task Register_player_returns_ok_when_command_succeeds()
    {
        using WebApplicationFactory<Program> factory = CreateFactory(_ => Result.Success());
        HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync(
            "/api/players/register",
            new RegisterPlayerRequest(Guid.NewGuid(), "UCL"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Register_player_returns_bad_request_when_request_is_invalid()
    {
        using WebApplicationFactory<Program> factory = CreateFactory(_ => Result.Success());
        HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync(
            "/api/players/register",
            new RegisterPlayerRequest(Guid.Empty, "UCL"));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_player_returns_problem_when_dispatcher_throws()
    {
        using WebApplicationFactory<Program> factory = CreateFactory(_ => throw new InvalidOperationException("boom"));
        HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync(
            "/api/players/register",
            new RegisterPlayerRequest(Guid.NewGuid(), "UCL"));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    private static WebApplicationFactory<Program> CreateFactory(Func<object, Result> handler)
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.RemoveAll<ICommandDispatcher>();
                services.AddScoped<ICommandDispatcher>(_ => new FakeCommandDispatcher(handler));
            }));
    }

    private sealed class FakeCommandDispatcher : ICommandDispatcher
    {
        private readonly Func<object, Result> _handler;

        public FakeCommandDispatcher(Func<object, Result> handler)
        {
            _handler = handler;
        }

        public Task<Result> DispatchAsync<TCommand>(TCommand command)
        {
            return Task.FromResult(_handler(command!));
        }
    }
}
