using Mcm.Api.Configurables;
using Mcm.Implementation.Configurables;

namespace Mcm.Implementation;

internal partial class ModLayout : IModLayout
{
    public IToggle AddToggleOption(string key, string name, string description, Func<bool> get, Action<bool> set)
    {
        var registry = McmManager.GetMcmRegistry(Owner)
            ?? throw new InvalidOperationException($"{Owner.id} must be registerd with MCM first");
        var entry = new McmSettingEntry(Owner.I2Loc(name), Owner.I2Loc(description), false, IBasicEntry.EntryType.Toggle);

        if (!registry.Settings.TryAdd(key, entry)) {
            throw new ArgumentException($"key {key} already exist for {Owner.id}");
        }

        var mcmToggle = new McmToggle(key, entry) {
            Owner = Owner,
            Save = (value) => {
                set(value);
                Owner.SetMcmConfig(key, value);
            },
            Read = get,
        };
        IndexPage.Add(mcmToggle);
        return mcmToggle;
    }
}
