using domain_fh.Enums;

namespace domain_fh.Common.Result;

public abstract class ResultError(
    string code, string message, 
    EErrorCategory category, IEnumerable<string>? details = null)
{
    public string Code { get; } = code;
    public string Message { get; } = message;
    public EErrorCategory Category { get; } = category;
    public IEnumerable<string>? Details { get; } = details;

    public override string ToString() => $"{Code}: {Message}";
}