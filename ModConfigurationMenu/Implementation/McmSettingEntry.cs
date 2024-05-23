using Mcm.Api.Configurables;

namespace Mcm.Implementation;

#nullable enable

public sealed record McmSettingEntry
{
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public IBasicEntry.EntryType EntryType { get; set; } = IBasicEntry.EntryType.Unknown;
    public object Value { get; set; } = new();
    public float? Min { get; set; } = null;
    public float? Max { get; set; } = null;
    public float? Step { get; set; } = null;
    public string[]? Options { get; set; } = null;


    public McmSettingEntry(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
