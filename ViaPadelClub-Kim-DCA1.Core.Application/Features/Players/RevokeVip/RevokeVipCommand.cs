using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RevokeVip;

public sealed class RevokeVipCommand
{
    public PlayerId PlayerId { get; }
    public ManagerId ManagerId { get; }
    public string Reason { get; }

    private RevokeVipCommand(PlayerId playerId, ManagerId managerId, string reason)
    {
        PlayerId = playerId;
        ManagerId = managerId;
        Reason = reason;
    }

    public static Result<RevokeVipCommand> Create(Guid playerId)
    {
        if (playerId == Guid.Empty)
            return Result<RevokeVipCommand>.Failure(new Error("player.id.empty", "Player id cannot be empty"));

        return Result<RevokeVipCommand>.Success(new RevokeVipCommand(new PlayerId(playerId), new ManagerId(Guid.Empty), string.Empty));
    }

    public static Result<RevokeVipCommand> Create(Guid playerId, Guid managerId, string reason)
    {
        Result<RevokeVipCommand> baseResult = Create(playerId);
        if (baseResult.IsFailure)
            return baseResult;

        if (managerId == Guid.Empty)
            return Result<RevokeVipCommand>.Failure(new Error("manager.id.empty", "Manager id cannot be empty"));

        if (string.IsNullOrWhiteSpace(reason))
            return Result<RevokeVipCommand>.Failure(new Error("adminaction.reason.empty", "Reason cannot be empty"));

        return Result<RevokeVipCommand>.Success(new RevokeVipCommand(new PlayerId(playerId), new ManagerId(managerId), reason));
    }
}
