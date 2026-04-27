using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RevokeVip;

public sealed class RevokeVipCommand
{
    public PlayerId PlayerId { get; }

    private RevokeVipCommand(PlayerId playerId)
    {
        PlayerId = playerId;
    }

    public static Result<RevokeVipCommand> Create(Guid playerId)
    {
        if (playerId == Guid.Empty)
        {
            return Result<RevokeVipCommand>.Failure(
                new Error("player.id.empty", "Player id cannot be empty"));
        }

        return Result<RevokeVipCommand>.Success(new RevokeVipCommand(new PlayerId(playerId)));
    }
}