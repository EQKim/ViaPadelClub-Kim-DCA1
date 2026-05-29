using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Managers;

public sealed class Manager : AggregateRoot<ManagerId>
{
    public string PadelCompanyName { get; private set; }

    private Manager()
    {
        PadelCompanyName = default!;
    }

    private Manager(ManagerId id, string padelCompanyName) : base(id)
    {
        PadelCompanyName = padelCompanyName;
    }

    public static Result<Manager> Create(ManagerId id, string padelCompanyName)
    {
        if (string.IsNullOrWhiteSpace(padelCompanyName))
        {
            return Result<Manager>.Failure(
                new Error("manager.padel_company_name.empty", "Padel company name cannot be empty"));
        }

        return Result<Manager>.Success(new Manager(id, padelCompanyName));
    }
}
