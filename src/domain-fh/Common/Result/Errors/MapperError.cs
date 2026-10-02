using domain_fh.Enums;

namespace domain_fh.Common.Result.Errors;

public class MapperError : ResultError
{
    private MapperError(string code, string displayMessage, EErrorCategory category)
        : base(code, displayMessage, category) { }
    
    public static MapperError NullObject()
        => new("MAP_NULL_OBJECT", "", EErrorCategory.Validation);
    
    public static MapperError Validation()
        => new("MAP_VALIDATION", "", EErrorCategory.Validation);
}