using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;

public sealed class Player : AggregateRoot<PlayerId>
{
    public UniversityName UniversityName { get; private set; }
    public bool IsVip { get; private set; }
    public bool IsBanned { get; private set; }

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
        if (IsVip)
        {
            return Result.Failure(
                new Error("player.already_vip", "Player is already VIP")
            );
        }
        IsVip = true;
        return Result.Success();
    }
    
    public Result RevokeVip()
    {
        if (!IsVip)
        {
            return Result.Failure(
                new Error("player.not_vip", "Player is not VIP")
            );
        }

        IsVip = false;
        return Result.Success();
    }
    
    public Result Ban()
    {
        if (IsBanned)
        {
            return Result.Failure(
                new Error("player.already_banned", "Player is already banned")
            );
        }

        IsBanned = true;
        return Result.Success();
    }
}