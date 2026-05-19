namespace ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;

public interface IRepository<TAggregate, in TId>
{
    Task AddAsync(TAggregate aggregate);
    Task<TAggregate?> GetByIdAsync(TId id);
}
