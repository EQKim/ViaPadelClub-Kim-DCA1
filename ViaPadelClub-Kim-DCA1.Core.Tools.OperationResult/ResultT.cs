namespace ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, T? value, IEnumerable<Error>? errors = null)
        : base(isSuccess, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value);
    }

    public static new Result<T> Failure(params Error[] errors)
    {
        return new Result<T>(false, default, errors);
    }

}
