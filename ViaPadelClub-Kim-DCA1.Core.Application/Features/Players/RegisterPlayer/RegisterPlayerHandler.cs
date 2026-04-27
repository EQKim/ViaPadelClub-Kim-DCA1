using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RegisterPlayer;

public sealed class RegisterPlayerHandler : ICommandHandler<RegisterPlayerCommand>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterPlayerHandler(
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(RegisterPlayerCommand command)
    {
        Result<Player> playerResult = Player.Register(
            command.PlayerId,
            command.UniversityName);

        if (playerResult.IsFailure)
        {
            return Result.Failure(playerResult.Errors);
        }

        await _playerRepository.AddAsync(playerResult.Value!);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}