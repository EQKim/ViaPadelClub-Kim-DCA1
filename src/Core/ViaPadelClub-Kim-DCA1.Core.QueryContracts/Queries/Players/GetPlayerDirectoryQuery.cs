namespace ViaPadelClub_Kim_DCA1.Core.QueryContracts.Queries.Players;

public sealed record GetPlayerDirectoryQuery(
    bool? IsVip = null,
    bool? IsBanned = null);

public sealed record PlayerDirectoryAnswer(
    IReadOnlyList<PlayerDirectoryItemDto> Players);

public sealed record PlayerDirectoryItemDto(
    Guid PlayerId,
    string UniversityName,
    bool IsVip,
    bool IsBanned);
