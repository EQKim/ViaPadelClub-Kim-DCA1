using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.BanPlayer;

public sealed class BanPlayerHandler : ICommandHandler<BanPlayerCommand>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BanPlayerHandler(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(BanPlayerCommand command)
    {
        Player? player = await _playerRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
        {
            return Result.Failure(
                new Error("player.not_found", "Player was not found"));
        }

        Result result = player.Ban();

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}