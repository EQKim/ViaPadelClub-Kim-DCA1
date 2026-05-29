using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Courts.CreateCourt;

public sealed class CreateCourtHandler : ICommandHandler<CreateCourtCommand>
{
    private readonly ICourtRepository _courtRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourtHandler(ICourtRepository courtRepository, IUnitOfWork unitOfWork)
    {
        _courtRepository = courtRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(CreateCourtCommand command)
    {
        Result<Court> result = Court.Create(command.CourtId, command.CourtName);

        if (result.IsFailure)
            return Result.Failure(result.Errors);

        await _courtRepository.AddAsync(result.Value!);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
