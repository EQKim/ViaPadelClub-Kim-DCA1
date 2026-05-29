using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts;

public sealed class Court : AggregateRoot<CourtId>
{
    public string CourtName { get; private set; }

    private Court()
    {
        CourtName = default!;
    }

    private Court(CourtId id, string courtName) : base(id)
    {
        CourtName = courtName;
    }

    public static Result<Court> Create(CourtId id, string courtName)
    {
        if (string.IsNullOrWhiteSpace(courtName))
            return Result<Court>.Failure(new Error("court.name.empty", "Court name cannot be empty"));

        return Result<Court>.Success(new Court(id, courtName));
    }
}
