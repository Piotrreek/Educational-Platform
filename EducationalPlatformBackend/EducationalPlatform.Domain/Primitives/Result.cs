namespace EducationalPlatform.Domain.Primitives;

public class Result
{
    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrWhiteSpace(error))
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && string.IsNullOrWhiteSpace(error))
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }


    public static Result Ok() => new(true, string.Empty);
    
    public static Result<TValue> Ok<TValue>(TValue? value) where TValue : class => new(value, true, string.Empty);

    public static Result Fail(string error) => new(false, error);

    public static Result<TValue> Fail<TValue>(string error) where TValue : class => new(default, false, error);
}

public class Result<TValue> : Result
    where TValue : class
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, string error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue? Value()
    {
        if (IsFailure)
        {
            throw new InvalidOperationException();
        }

        return _value;
    }
}