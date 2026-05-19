using Microsoft.EntityFrameworkCore;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Repositories;

public abstract class Repository<TAggregate, TId> : IRepository<TAggregate, TId>
    where TAggregate : class
{
    protected readonly DmContext Context;

    protected Repository(DmContext context)
    {
        Context = context;
    }

    public Task AddAsync(TAggregate aggregate)
    {
        Context.Set<TAggregate>().Add(aggregate);
        return Task.CompletedTask;
    }

    public virtual Task<TAggregate?> GetByIdAsync(TId id)
    {
        return Context.Set<TAggregate>().FindAsync(id).AsTask();
    }
}
