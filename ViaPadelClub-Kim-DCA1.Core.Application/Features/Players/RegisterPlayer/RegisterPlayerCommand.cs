using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RegisterPlayer;

public sealed class RegisterPlayerCommand
{
    public PlayerId PlayerId { get; }
    public UniversityName UniversityName { get; }

    private RegisterPlayerCommand(PlayerId playerId, UniversityName universityName)
    {
        PlayerId = playerId;
        UniversityName = universityName;
    }

    public static Result<RegisterPlayerCommand> Create(Guid playerId, string universityName)
    {
        if (playerId == Guid.Empty)
        {
            return Result<RegisterPlayerCommand>.Failure(
                new Error("player.id.empty", "Player id cannot be empty"));
        }

        if (string.IsNullOrWhiteSpace(universityName))
        {
            return Result<RegisterPlayerCommand>.Failure(
                new Error("player.university.empty", "University name cannot be empty"));
        }

        RegisterPlayerCommand command = new(
            new PlayerId(playerId),
            new UniversityName(universityName));

        return Result<RegisterPlayerCommand>.Success(command);
    }
}