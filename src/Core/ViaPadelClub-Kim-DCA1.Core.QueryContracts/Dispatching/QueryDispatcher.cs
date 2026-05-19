namespace ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;

public sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly Dictionary<Type, object> _handlers = new();

    public QueryDispatcher Register<TQuery, TAnswer>(IQueryHandler<TQuery, TAnswer> handler)
    {
        _handlers[typeof(TQuery)] = handler;
        return this;
    }

    public Task<TAnswer> DispatchAsync<TQuery, TAnswer>(TQuery query)
    {
        if (!_handlers.TryGetValue(typeof(TQuery), out object? handler))
        {
            throw new InvalidOperationException(
                $"No query handler was registered for {typeof(TQuery).Name}.");
        }

        return ((IQueryHandler<TQuery, TAnswer>)handler).HandleAsync(query);
    }
}
