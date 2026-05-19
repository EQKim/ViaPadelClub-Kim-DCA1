using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;

public sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<Type, object> _handlers = new();

    public CommandDispatcher Register<TCommand>(ICommandHandler<TCommand> handler)
    {
        _handlers[typeof(TCommand)] = handler;
        return this;
    }

    public Task<Result> DispatchAsync<TCommand>(TCommand command)
    {
        if (!_handlers.TryGetValue(typeof(TCommand), out object? handler))
        {
            return Task.FromResult(Result.Failure(
                new Error("command.handler.not_found", $"No handler was registered for {typeof(TCommand).Name}")));
        }

        return ((ICommandHandler<TCommand>)handler).HandleAsync(command);
    }
}
