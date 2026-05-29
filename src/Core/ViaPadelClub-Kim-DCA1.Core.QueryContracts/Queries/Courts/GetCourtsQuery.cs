namespace ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.Courts;

public sealed record GetCourtsQuery;

public sealed record CourtsAnswer(IReadOnlyList<CourtDto> Courts);

public sealed record CourtDto(Guid CourtId, string CourtName);
