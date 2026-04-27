using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

namespace ViaPadelClub_Kim_DCA1.Core.Application.Abstractions;

public interface ICommandHandler<TCommand>
{
    Task<Result> HandleAsync(TCommand command);
}