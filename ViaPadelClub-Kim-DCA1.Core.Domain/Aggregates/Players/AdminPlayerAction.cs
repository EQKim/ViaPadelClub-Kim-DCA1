using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;

public sealed class AdminPlayerAction : Entity<Guid>
{
    public string ActionType { get; private set; }
    public ManagerId ManagerId { get; private set; }
    public string Reason { get; private set; }
    public DateTime Timestamp { get; private set; }

    private AdminPlayerAction()
    {
        ActionType = default!;
        ManagerId = default!;
        Reason = default!;
    }

    public AdminPlayerAction(Guid id, string actionType, ManagerId managerId, string reason, DateTime timestamp)
        : base(id)
    {
        ActionType = actionType;
        ManagerId = managerId;
        Reason = reason;
        Timestamp = timestamp;
    }
}
