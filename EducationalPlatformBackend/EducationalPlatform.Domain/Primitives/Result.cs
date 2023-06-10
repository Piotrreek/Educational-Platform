namespace EducationalPlatform.Domain.Primitives;

public class Result
{
    protected Result(bool isSuccess, string error, int statusCode)
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
        StatusCode = statusCode;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    public int StatusCode { get; }


    public static Result Ok(int statusCode = 200) => new(true, string.Empty, statusCode);
    
    public static Result<TValue> Ok<TValue>(TValue? value, int statusCode = 200) where TValue : class => new(value, true, string.Empty, statusCode);

    public static Result Fail(string error, int statusCode) => new(false, error, statusCode);

    public static Result<TValue> Fail<TValue>(string error, int statusCode) where TValue : class => new(default, false, error, statusCode);
}

public class Result<TValue> : Result
    where TValue : class
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, string error, int statusCode)
        : base(isSuccess, error, statusCode)
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