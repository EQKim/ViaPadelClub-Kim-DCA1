using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Repositories;

namespace IntegrationTests.Repositories;

public sealed class PlayerRepositoryTests
{
    [NeonFact]
    public async Task AddAndGetByIdAsync_ShouldPersistPlayerState()
    {
        await using DmContext context = DatabaseConnection.CreateContext();
        await context.Database.EnsureCreatedAsync();

        Player player = Player.Register(
            new PlayerId(Guid.NewGuid()),
            new UniversityName("VIA University College")).Value!;

        player.GrantVip();
        player.Ban();

        PlayerRepository repository = new(context);
        UnitOfWork unitOfWork = new(context);

        await repository.AddAsync(player);
        await unitOfWork.SaveChangesAsync();

        context.ChangeTracker.Clear();

        Player? loaded = await repository.GetByIdAsync(player.Id);

        Assert.NotNull(loaded);
        Assert.Equal(player.Id, loaded.Id);
        Assert.Equal("VIA University College", loaded.UniversityName.Value);
        Assert.True(loaded.IsVip);
        Assert.True(loaded.IsBanned);
    }
}
