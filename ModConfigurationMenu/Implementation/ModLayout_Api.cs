using ChronoArkMod;
using Mcm.Api.Configurables;
using Mcm.Implementation.Configurables;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation;

internal partial class ModLayout : IModLayout
{
    public IToggle AddToggleOption(string key, string name, string description, bool @default)
    {
        var registry = McmManager.GetMcmRegistry(Owner)
            ?? throw new InvalidOperationException($"{Owner.id} must be registerd with MCM first");
        var entry = new McmSettingEntry(Owner.I2Loc(name), Owner.I2Loc(description), @default, IBasicEntry.EntryType.Toggle);
        
        if (!registry.Settings.TryAdd(key, entry)) {
            throw new ArgumentException($"key {key} already exist for {Owner.id}");
        }
        var mcmToggle = new McmToggle(key, entry) {
            Owner = Owner,
            Save = (value) => McmManager.UpdateModSetting(Owner, key, value),
            Read = () => McmManager.GetModSetting<bool>(Owner, key),
        };
        IndexPage.Add(mcmToggle);
        return mcmToggle;
    }
}
