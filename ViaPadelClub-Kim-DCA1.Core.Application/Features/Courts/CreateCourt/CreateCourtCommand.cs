using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Courts.CreateCourt;

public sealed class CreateCourtCommand
{
    public CourtId CourtId { get; }
    public string CourtName { get; }

    private CreateCourtCommand(CourtId courtId, string courtName)
    {
        CourtId = courtId;
        CourtName = courtName;
    }

    public static Result<CreateCourtCommand> Create(Guid courtId, string courtName)
    {
        if (courtId == Guid.Empty)
            return Result<CreateCourtCommand>.Failure(new Error("court.id.empty", "Court id cannot be empty"));

        if (string.IsNullOrWhiteSpace(courtName))
            return Result<CreateCourtCommand>.Failure(new Error("court.name.empty", "Court name cannot be empty"));

        return Result<CreateCourtCommand>.Success(new CreateCourtCommand(new CourtId(courtId), courtName));
    }
}
