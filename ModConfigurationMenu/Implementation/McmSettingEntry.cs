using Mcm.Api.Configurables;

namespace Mcm.Implementation;

internal sealed record McmSettingEntry
{
    public string Name;
    public string Description;
    public object Value;
    public IBasicEntry.EntryType EntryType;

    public McmSettingEntry()
    {
    }

    public McmSettingEntry(string name, string description, object value, IBasicEntry.EntryType entryType)
    {
        Name = name;
        Description = description;
        Value = value;
        EntryType = entryType;
    }
}
