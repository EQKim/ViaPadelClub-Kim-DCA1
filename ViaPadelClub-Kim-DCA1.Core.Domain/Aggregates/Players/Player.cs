using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;

public sealed class Player : AggregateRoot<PlayerId>
{
    private readonly List<AdminPlayerAction> _adminActions = new();

    public UniversityName UniversityName { get; private set; }
    public bool IsVip { get; private set; }
    public bool IsBanned { get; private set; }
    public IReadOnlyList<AdminPlayerAction> AdminActions => _adminActions;

    private Player()
    {
        UniversityName = default!;
    }

    private Player(PlayerId id, UniversityName universityName) : base(id)
    {
        UniversityName = universityName;
        IsVip = false;
        IsBanned = false;
    }

    public static Result<Player> Register(PlayerId id, UniversityName universityName)
    {
        if (string.IsNullOrWhiteSpace(universityName.Value))
        {
            return Result<Player>.Failure(
                new Error("player.university.empty", "University name cannot be empty")
            );
        }

        Player player = new Player(id, universityName);
        return Result<Player>.Success(player);
    }

    public Result GrantVip()
    {
        return GrantVip(new ManagerId(Guid.Empty), string.Empty);
    }

    public Result GrantVip(ManagerId managerId, string reason)
    {
        if (IsVip)
        {
            return Result.Failure(
                new Error("player.already_vip", "Player is already VIP")
            );
        }

        IsVip = true;
        AddAdminAction("VipGranted", managerId, reason);
        return Result.Success();
    }

    public Result RevokeVip()
    {
        return RevokeVip(new ManagerId(Guid.Empty), string.Empty);
    }

    public Result RevokeVip(ManagerId managerId, string reason)
    {
        if (!IsVip)
        {
            return Result.Failure(
                new Error("player.not_vip", "Player is not VIP")
            );
        }

        IsVip = false;
        AddAdminAction("VipRevoked", managerId, reason);
        return Result.Success();
    }

    public Result Ban()
    {
        return Ban(new ManagerId(Guid.Empty), string.Empty);
    }

    public Result Ban(ManagerId managerId, string reason)
    {
        if (IsBanned)
        {
            return Result.Failure(
                new Error("player.already_banned", "Player is already banned")
            );
        }

        IsBanned = true;
        AddAdminAction("Banned", managerId, reason);
        return Result.Success();
    }

    public Result Unban()
    {
        return Unban(new ManagerId(Guid.Empty), string.Empty);
    }

    public Result Unban(ManagerId managerId, string reason)
    {
        if (!IsBanned)
        {
            return Result.Failure(
                new Error("player.not_banned", "Player is not banned")
            );
        }

        IsBanned = false;
        AddAdminAction("Unbanned", managerId, reason);
        return Result.Success();
    }

    private void AddAdminAction(string actionType, ManagerId managerId, string reason)
    {
        if (managerId.Value == Guid.Empty && string.IsNullOrWhiteSpace(reason))
            return;

        _adminActions.Add(new AdminPlayerAction(
            Guid.NewGuid(),
            actionType,
            managerId,
            reason,
            DateTime.UtcNow));
    }
}
