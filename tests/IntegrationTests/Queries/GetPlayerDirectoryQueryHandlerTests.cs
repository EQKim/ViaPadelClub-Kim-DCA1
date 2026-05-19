using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.Players;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries;
using ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Handlers;

namespace IntegrationTests.Queries;

public sealed class GetPlayerDirectoryQueryHandlerTests
{
    [NeonFact]
    public async Task HandleAsync_WithVipFilter_ShouldReturnVipPlayers()
    {
        await using QueryDbContext context = QueryDatabaseConnection.CreateContext();
        await context.Database.EnsureCreatedAsync();
        await QuerySeedData.ResetAssignment8DataAsync(context);
        await QuerySeedData.SeedPlayersAsync(context);

        GetPlayerDirectoryQueryHandler handler = new(context);

        PlayerDirectoryAnswer answer =
            await handler.HandleAsync(new GetPlayerDirectoryQuery(IsVip: true));

        Assert.NotEmpty(answer.Players);
        Assert.All(answer.Players, player => Assert.True(player.IsVip));
        Assert.Contains(answer.Players, player => player.UniversityName == "Assignment 8 VIA");
    }
}
