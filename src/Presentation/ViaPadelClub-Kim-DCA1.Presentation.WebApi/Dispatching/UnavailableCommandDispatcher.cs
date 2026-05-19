using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Dispatching;

public sealed class UnavailableCommandDispatcher : ICommandDispatcher
{
    public Task<Result> DispatchAsync<TCommand>(TCommand command)
    {
        return Task.FromResult(Result.Failure(
            new Error("database.connection_string.missing", "Postgres connection string is not configured.")));
    }
}
