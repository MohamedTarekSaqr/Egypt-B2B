namespace EgyptB2B.Application.Common.Models;

public class Result
{
    protected Result(bool isSuccess, IReadOnlyCollection<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public IReadOnlyCollection<Error> Errors { get; }

    public static Result Success() => new(true, Array.Empty<Error>());

    public static Result Failure(params Error[] errors) => new(false, errors);
}

public sealed class Result<T> : Result
{
    private Result(T value)
        : base(true, Array.Empty<Error>())
    {
        Value = value;
    }

    private Result(IReadOnlyCollection<Error> errors)
        : base(false, errors)
    {
    }

    public T? Value { get; }

    public static Result<T> Success(T value) => new(value);

    public static new Result<T> Failure(params Error[] errors) => new(errors);
}
