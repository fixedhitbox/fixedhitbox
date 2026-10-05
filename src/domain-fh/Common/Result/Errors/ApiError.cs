using domain_fh.Enums;

namespace domain_fh.Common.Result.Errors;

public sealed class ApiError : ResultError
{
    private ApiError(
        string code, 
        string displayMessage, 
        EErrorCategory category, 
        string refCode)
        : base(code, displayMessage, category, errorReference: refCode) { }
    
    public static ApiError NotFound(string reference)
        => new("API_NOT_FOUND", "", EErrorCategory.NotFound, reference);
    
    public static ApiError InvalidResponse(string reference)
        => new("API_INVALID_RESPONSE", "", EErrorCategory.Validation, reference);
    
    public static ApiError Timeout(string reference)
        => new("API_TIMEOUT", "", EErrorCategory.Timeout, reference);
    
    public static ApiError CanceledOperation(string reference)
        => new("API_CANCELED_OPERATION", "", EErrorCategory.CanceledOperation, reference);
    
    public static ApiError RateLimited(string displayMessage, string reference)
        => new("API_RATE_LIMITED", displayMessage, EErrorCategory.Limited, reference);
    
    public static ApiError Unexpected(string displayMessage, string reference)
        => new("API_UNEXPECTED", displayMessage, EErrorCategory.Unexpected, reference);
}