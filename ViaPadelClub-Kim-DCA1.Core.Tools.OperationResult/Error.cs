namespace ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}