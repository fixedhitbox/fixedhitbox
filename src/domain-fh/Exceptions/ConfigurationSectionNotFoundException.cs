namespace domain_fh.Exceptions;

public sealed class ConfigurationSectionNotFoundException (
    string sectionName, Type optionsType) : Exception
    ($"Configuration section '{sectionName}' not found for '{optionsType.Name}'.")
{
    public string SectionName { get; } = sectionName;
    public Type OptionsType { get; } = optionsType;
}