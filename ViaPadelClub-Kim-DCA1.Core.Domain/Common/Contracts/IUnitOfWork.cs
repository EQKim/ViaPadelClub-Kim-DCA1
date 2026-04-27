namespace ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}