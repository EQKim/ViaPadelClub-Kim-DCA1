using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Managers.CreateManager;

public sealed class CreateManagerCommand
{
    public ManagerId ManagerId { get; }
    public string PadelCompanyName { get; }

    private CreateManagerCommand(ManagerId managerId, string padelCompanyName)
    {
        ManagerId = managerId;
        PadelCompanyName = padelCompanyName;
    }

    public static Result<CreateManagerCommand> Create(Guid managerId, string padelCompanyName)
    {
        if (managerId == Guid.Empty)
            return Result<CreateManagerCommand>.Failure(new Error("manager.id.empty", "Manager id cannot be empty"));

        if (string.IsNullOrWhiteSpace(padelCompanyName))
            return Result<CreateManagerCommand>.Failure(new Error("manager.padel_company_name.empty", "Padel company name cannot be empty"));

        return Result<CreateManagerCommand>.Success(
            new CreateManagerCommand(new ManagerId(managerId), padelCompanyName));
    }
}
