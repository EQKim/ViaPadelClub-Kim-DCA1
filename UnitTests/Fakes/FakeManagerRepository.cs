using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Managers;

namespace UnitTests.Fakes;

public sealed class FakeManagerRepository : IManagerRepository
{
    private readonly List<Manager> _managers = new();

    public IReadOnlyList<Manager> Managers => _managers;

    public Task AddAsync(Manager manager)
    {
        _managers.Add(manager);
        return Task.CompletedTask;
    }

    public Task<Manager?> GetByIdAsync(ManagerId id)
    {
        Manager? manager = _managers.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(manager);
    }
}
