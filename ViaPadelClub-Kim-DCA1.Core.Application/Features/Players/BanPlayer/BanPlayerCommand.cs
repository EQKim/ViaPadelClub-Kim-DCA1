using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.BanPlayer;

public sealed class BanPlayerCommand
{
    public PlayerId PlayerId { get; }

    private BanPlayerCommand(PlayerId playerId)
    {
        PlayerId = playerId;
    }

    public static Result<BanPlayerCommand> Create(Guid playerId)
    {
        if (playerId == Guid.Empty)
        {
            return Result<BanPlayerCommand>.Failure(
                new Error("player.id.empty", "Player id cannot be empty"));
        }

        return Result<BanPlayerCommand>.Success(
            new BanPlayerCommand(new PlayerId(playerId)));
    }
}