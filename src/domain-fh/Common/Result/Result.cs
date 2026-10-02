namespace domain_fh.Common.Result;

public class Result<TError> where TError : ResultError
{
    public bool IsSuccess { get; }
    public TError? Error { get; }
    public string? Message { get; }
    public IEnumerable<string>? Details { get; }

    protected Result(bool isSuccess, TError? error, string? message = null, IEnumerable<string>? details = null)
    {
        switch (isSuccess)
        {
            case true when error is not null:
            case false when error is null: 
                throw new InvalidOperationException(
                    "Success result cannot have an error, or a failure result must specify it.");
        }
        
        IsSuccess = isSuccess;
        Error = error;
        Message = message;
        Details = details;
    }
}

public sealed class Result<T, TError> : Result<TError> where TError : ResultError
{ 
    private readonly T? _value;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access 'Value' from a failed result.");
    
    private Result(bool isSuccess, T? value, TError? error, string? message = null, IEnumerable<string>? details = null)
        : base(isSuccess, error, message, details) => _value = value;
    
    public static Result<T, TError> Success(T value, string? message = null) => new(true, value, null, message);
    public static Result<T, TError> Failure(TError error, string message, IEnumerable<string>? details = null) => new(false, default, error, message, details);
}