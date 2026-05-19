namespace ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;

public interface IQueryDispatcher
{
    Task<TAnswer> DispatchAsync<TQuery, TAnswer>(TQuery query);
}
