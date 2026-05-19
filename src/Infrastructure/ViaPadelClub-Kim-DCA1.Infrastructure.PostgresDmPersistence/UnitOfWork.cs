using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DmContext _context;

    public UnitOfWork(DmContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
