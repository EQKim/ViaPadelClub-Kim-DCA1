using ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Contracts;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Features.Players.RevokeVip;

public sealed class RevokeVipHandler : ICommandHandler<RevokeVipCommand>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RevokeVipHandler(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(RevokeVipCommand command)
    {
        Player? player = await _playerRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
        {
            return Result.Failure(
                new Error("player.not_found", "Player was not found"));
        }

        Result result = player.RevokeVip();

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}