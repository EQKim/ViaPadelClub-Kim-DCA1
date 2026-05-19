using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Dispatching;

public sealed class CommandDispatcherTests
{
    [Fact]
    public async Task DispatchAsync_WithRegisteredHandler_ShouldDelegateCommandToHandler()
    {
        CommandDispatcher dispatcher = new();
        TestCommand command = new("register-player");
        SpyCommandHandler<TestCommand> handler = new(Result.Success());

        dispatcher.Register(handler);

        Result result = await dispatcher.DispatchAsync(command);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, handler.CallCount);
        Assert.Same(command, handler.LastCommand);
    }

    [Fact]
    public async Task DispatchAsync_WithMultipleHandlers_ShouldCallHandlerForMatchingCommandType()
    {
        CommandDispatcher dispatcher = new();
        SpyCommandHandler<TestCommand> matchingHandler = new(Result.Success());
        SpyCommandHandler<OtherCommand> otherHandler = new(Result.Success());

        dispatcher.Register(matchingHandler);
        dispatcher.Register(otherHandler);

        Result result = await dispatcher.DispatchAsync(new TestCommand("grant-vip"));

        Assert.True(result.IsSuccess);
        Assert.Equal(1, matchingHandler.CallCount);
        Assert.Equal(0, otherHandler.CallCount);
    }

    [Fact]
    public async Task DispatchAsync_WithoutRegisteredHandler_ShouldFail()
    {
        CommandDispatcher dispatcher = new();

        Result result = await dispatcher.DispatchAsync(new TestCommand("unknown"));

        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "command.handler.not_found");
    }

    private sealed record TestCommand(string Name);

    private sealed record OtherCommand(string Name);

    private sealed class SpyCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        private readonly Result _result;

        public SpyCommandHandler(Result result)
        {
            _result = result;
        }

        public int CallCount { get; private set; }
        public TCommand? LastCommand { get; private set; }

        public Task<Result> HandleAsync(TCommand command)
        {
            CallCount++;
            LastCommand = command;
            return Task.FromResult(_result);
        }
    }
}
