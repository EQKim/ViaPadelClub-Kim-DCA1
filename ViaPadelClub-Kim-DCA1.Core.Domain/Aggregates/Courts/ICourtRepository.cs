using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts;

public interface ICourtRepository : IRepository<Court, CourtId>
{
}
