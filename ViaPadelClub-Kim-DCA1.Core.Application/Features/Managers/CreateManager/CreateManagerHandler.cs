using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Managers;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Managers.CreateManager;

public sealed class CreateManagerHandler : ICommandHandler<CreateManagerCommand>
{
    private readonly IManagerRepository _managerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateManagerHandler(IManagerRepository managerRepository, IUnitOfWork unitOfWork)
    {
        _managerRepository = managerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(CreateManagerCommand command)
    {
        Result<Manager> result = Manager.Create(command.ManagerId, command.PadelCompanyName);

        if (result.IsFailure)
            return Result.Failure(result.Errors);

        await _managerRepository.AddAsync(result.Value!);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
