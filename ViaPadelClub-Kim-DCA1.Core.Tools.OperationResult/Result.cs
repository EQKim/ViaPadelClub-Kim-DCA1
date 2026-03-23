namespace ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

public class Result
{
    private readonly List<Error> _errors = new();

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public IReadOnlyList<Error> Errors => _errors;

    protected Result(bool isSuccess, IEnumerable<Error>? errors = null)
    {
        IsSuccess = isSuccess;

        if (errors != null)
            _errors.AddRange(errors);
    }

    public static Result Success()
    {
        return new Result(true);
    }

    public static Result Failure(params Error[] errors)
    {
        return new Result(false, errors);
    }

    public static Result Failure(IEnumerable<Error> errors)
    {
        return new Result(false, errors);
    }
}