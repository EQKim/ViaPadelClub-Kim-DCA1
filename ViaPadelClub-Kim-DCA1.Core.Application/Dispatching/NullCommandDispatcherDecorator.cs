using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;

public sealed class NullCommandDispatcherDecorator : ICommandDispatcher
{
    private readonly ICommandDispatcher _inner;

    public NullCommandDispatcherDecorator(ICommandDispatcher inner)
    {
        _inner = inner;
    }

    public Task<Result> DispatchAsync<TCommand>(TCommand command)
    {
        if (command is null)
        {
            return Task.FromResult(Result.Failure(
                new Error("command.null", "Command cannot be null")));
        }

        return _inner.DispatchAsync(command);
    }
}
