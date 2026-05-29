using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresDmPersistence.Repositories;

public sealed class CourtRepository : Repository<Court, CourtId>, ICourtRepository
{
    public CourtRepository(DmContext context) : base(context)
    {
    }
}
