using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;

namespace ViaPadelClub_Kim_DCA1.Presentation.WebApi.Dispatching;

public sealed class UnavailableQueryDispatcher : IQueryDispatcher
{
    public Task<TAnswer> DispatchAsync<TQuery, TAnswer>(TQuery query)
    {
        throw new InvalidOperationException("Postgres connection string is not configured.");
    }
}
