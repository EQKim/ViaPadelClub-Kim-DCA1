using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Time;

namespace ViaPadelClub_Kim_DCA1.Infrastructure.PostgresQueries.Time;

public sealed class SystemTime : ISystemTime
{
    public DateTime CurrentTime()
    {
        return DateTime.Now;
    }
}
