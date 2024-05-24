using Mcm.Api.Configurables;

namespace Mcm.Implementation;

public sealed record McmSettingEntry
{
    public McmSettingEntry(string key, string name, string description)
    {
        Key = key;
        Name = string.IsNullOrEmpty(name) ? key : name;
        Description = string.IsNullOrEmpty(description) ? McmLoc.Setting.Placeholder : description;
    }

    public string Key { get; init; }
    public string Name { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public IBasicEntry.EntryType EntryType { get; set; } = IBasicEntry.EntryType.Unknown;
    public object Value { get; set; } = new();
    public float? Min { get; set; }
    public float? Max { get; set; }
    public float? Step { get; set; }
    public string[]? Options { get; set; } = null;
}