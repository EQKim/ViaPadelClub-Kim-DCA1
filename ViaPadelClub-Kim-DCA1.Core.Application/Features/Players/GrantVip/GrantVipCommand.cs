using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.GrantVip;

public sealed class GrantVipCommand
{
    public PlayerId PlayerId { get; }

    private GrantVipCommand(PlayerId playerId)
    {
        PlayerId = playerId;
    }

    public static Result<GrantVipCommand> Create(Guid playerId)
    {
        if (playerId == Guid.Empty)
        {
            return Result<GrantVipCommand>.Failure(
                new Error("player.id.empty", "Player id cannot be empty"));
        }

        GrantVipCommand command = new(new PlayerId(playerId));

        return Result<GrantVipCommand>.Success(command);
    }
}