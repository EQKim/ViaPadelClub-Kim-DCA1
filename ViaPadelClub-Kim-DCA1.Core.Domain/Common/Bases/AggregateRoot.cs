namespace ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;

public abstract class AggregateRoot<TId> : Entity<TId>
{
    protected AggregateRoot()
    {
    }

    protected AggregateRoot(TId id) : base(id)
    {
    }
}
