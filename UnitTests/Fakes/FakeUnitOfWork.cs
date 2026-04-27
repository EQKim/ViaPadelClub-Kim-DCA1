using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;

namespace UnitTests.Fakes;

public sealed class FakeUnitOfWork : IUnitOfWork
{
    public bool SaveChangesWasCalled { get; private set; }

    public Task SaveChangesAsync()
    {
        SaveChangesWasCalled = true;
        return Task.CompletedTask;
    }
}