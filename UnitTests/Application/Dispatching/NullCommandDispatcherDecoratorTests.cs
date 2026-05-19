using ViaPadelClub_Kim_DCA1.Core.Application.Dispatching;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace UnitTests.Application.Dispatching;

public sealed class NullCommandDispatcherDecoratorTests
{
    [Fact]
    public async Task DispatchAsync_WithNullCommand_ShouldFailWithoutCallingInnerDispatcher()
    {
        SpyCommandDispatcher inner = new(Result.Success());
        NullCommandDispatcherDecorator decorator = new(inner);

        Result result = await decorator.DispatchAsync<TestCommand?>(null);

        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "command.null");
        Assert.Equal(0, inner.CallCount);
    }

    [Fact]
    public async Task DispatchAsync_WithCommand_ShouldDelegateToInnerDispatcher()
    {
        SpyCommandDispatcher inner = new(Result.Success());
        NullCommandDispatcherDecorator decorator = new(inner);
        TestCommand command = new("cancel-booking");

        Result result = await decorator.DispatchAsync(command);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, inner.CallCount);
        Assert.Same(command, inner.LastCommand);
    }

    private sealed record TestCommand(string Name);

    private sealed class SpyCommandDispatcher : ICommandDispatcher
    {
        private readonly Result _result;

        public SpyCommandDispatcher(Result result)
        {
            _result = result;
        }

        public int CallCount { get; private set; }
        public object? LastCommand { get; private set; }

        public Task<Result> DispatchAsync<TCommand>(TCommand command)
        {
            CallCount++;
            LastCommand = command;
            return Task.FromResult(_result);
        }
    }
}
