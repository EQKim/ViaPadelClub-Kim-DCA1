using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;

namespace UnitTests.Application.Dispatching;

public sealed class QueryDispatcherTests
{
    [Fact]
    public async Task DispatchAsync_WithRegisteredHandler_ShouldDelegateQueryToHandler()
    {
        QueryDispatcher dispatcher = new();
        SpyQueryHandler<TestQuery, TestAnswer> handler = new(new TestAnswer("handled"));
        TestQuery query = new("upcoming-schedules");

        dispatcher.Register<TestQuery, TestAnswer>(handler);

        TestAnswer answer = await dispatcher.DispatchAsync<TestQuery, TestAnswer>(query);

        Assert.Equal("handled", answer.Value);
        Assert.Equal(1, handler.CallCount);
        Assert.Same(query, handler.LastQuery);
    }

    [Fact]
    public async Task DispatchAsync_WithoutRegisteredHandler_ShouldThrow()
    {
        QueryDispatcher dispatcher = new();

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => dispatcher.DispatchAsync<TestQuery, TestAnswer>(new TestQuery("missing")));
    }

    private sealed record TestQuery(string Value);

    private sealed record TestAnswer(string Value);

    private sealed class SpyQueryHandler<TQuery, TAnswer> : IQueryHandler<TQuery, TAnswer>
    {
        private readonly TAnswer _answer;

        public SpyQueryHandler(TAnswer answer)
        {
            _answer = answer;
        }

        public int CallCount { get; private set; }
        public TQuery? LastQuery { get; private set; }

        public Task<TAnswer> HandleAsync(TQuery query)
        {
            CallCount++;
            LastQuery = query;
            return Task.FromResult(_answer);
        }
    }
}
