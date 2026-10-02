using domain_fh.Enums;

namespace domain_fh.Common.Result.Errors;

public sealed class ApiError : ResultError
{
    private ApiError(string code, string displayMessage, EErrorCategory category)
        : base(code, displayMessage, category) { }
    
    public static ApiError NotFound()
        => new("API_NOT_FOUND", "", EErrorCategory.NotFound);
    
    public static ApiError InvalidResponse()
        => new("API_INVALID_RESPONSE", "", EErrorCategory.Validation);
    
    public static ApiError Timeout()
        => new("API_TIMEOUT", "", EErrorCategory.Timeout);
    
    public static ApiError CanceledOperation()
        => new("API_CANCELED_OPERATION", "", EErrorCategory.CanceledOperation);
    
    public static ApiError RateLimited(string displayMessage)
        => new("API_RATE_LIMITED", displayMessage, EErrorCategory.Limited);
    
    public static ApiError Unexpected(string displayMessage)
        => new("API_UNEXPECTED", displayMessage, EErrorCategory.Unexpected);
}