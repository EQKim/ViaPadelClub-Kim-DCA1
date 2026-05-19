namespace ViaPadelClub_Kim_DCA1.Core.QueryContracts.Dispatching;

public interface IQueryHandler<TQuery, TAnswer>
{
    Task<TAnswer> HandleAsync(TQuery query);
}
