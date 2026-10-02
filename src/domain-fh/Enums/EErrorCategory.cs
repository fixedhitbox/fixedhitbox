namespace domain_fh.Enums;

public enum EErrorCategory : byte
{
    NotFound,
    Timeout,
    Validation,
    Conflict,
    Unauthorized,
    Forbidden,
    Limited,
    CanceledOperation,
    Unexpected
}