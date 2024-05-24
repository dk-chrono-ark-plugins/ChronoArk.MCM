using ChronoArkMod;
using Mcm.Api.Configurables;

namespace Mcm.Implementation;

#nullable enable

public sealed record McmSettingEntry
{
    public string Key { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IBasicEntry.EntryType EntryType { get; set; } = IBasicEntry.EntryType.Unknown;
    public object Value { get; set; } = new();
    public float? Min { get; set; } = null;
    public float? Max { get; set; } = null;
    public float? Step { get; set; } = null;
    public string[]? Options { get; set; } = null;


    public McmSettingEntry(string key, string name, string description)
    {
        Key = key;
        Name = string.IsNullOrEmpty(name) ? key : name;
        Description = string.IsNullOrEmpty(description) ? McmLoc.Setting.Placeholder : description;
    }
}
