using ViaPadelClub_Kim_DCA1.Core.QueryContracts.Time;

namespace IntegrationTests.Queries;

public sealed class FakeSystemTime : ISystemTime
{
    private readonly DateTime _currentTime;

    public FakeSystemTime(DateTime currentTime)
    {
        _currentTime = currentTime;
    }

    public DateTime CurrentTime()
    {
        return _currentTime;
    }
}
