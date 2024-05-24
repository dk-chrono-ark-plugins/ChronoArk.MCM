using Mcm.Api.Configurables;
using Mcm.Implementation.Configurables;

namespace Mcm.Implementation;

#nullable enable

internal partial class ModLayout : IModLayout
{
    public IDropdown AddDropdownMenu(string key, string name, string description, Func<string[]> options, Action<int> set)
    {
        throw new NotImplementedException();
    }

    public IInputField AddInputField<T>(string key, string name, string description, string @default, Action<T> set)
    {
        throw new NotImplementedException();
    }

    public ISlider AddSliderOption(string key, string name, string description, float min, float max, float step, Action<float> set)
    {
        var registry = McmManager.GetMcmRegistry(Owner)
            ?? throw new InvalidOperationException($"{Owner.id} must be registerd with MCM first");
        var entry = new McmSettingEntry(Owner.I2Loc(name), Owner.I2Loc(description)) {
            EntryType = IBasicEntry.EntryType.Slider,
            Value = min,
        };

        if (!registry.Settings.TryAdd(key, entry)) {
            throw new ArgumentException($"key {key} already exist for {Owner.id}");
        }

        var mcmSlider = new McmSlider(key, entry) {
            Owner = Owner,
            Save = (value) => {
                set(value);
                Owner.SetMcmConfig(key, value);
            },
            Read = () => Owner.GetMcmConfig<float>(key),
            Min = min,
            Max = max,
            Step = step,
        };
        McmManager.ResetMcmConfig(Owner);
        set(Owner.GetMcmConfig<float>(key));
        IndexPage.Add(mcmSlider);
        return mcmSlider;
    }

    public IToggle AddToggleOption(string key, string name, string description, Action<bool> set)
    {
        var registry = McmManager.GetMcmRegistry(Owner)
            ?? throw new InvalidOperationException($"{Owner.id} must be registerd with MCM first");
        var entry = new McmSettingEntry(Owner.I2Loc(name), Owner.I2Loc(description)) {
            EntryType = IBasicEntry.EntryType.Toggle,
            Value = false,
        };

        if (!registry.Settings.TryAdd(key, entry)) {
            throw new ArgumentException($"key {key} already exist for {Owner.id}");
        }

        var mcmToggle = new McmToggle(key, entry) {
            Owner = Owner,
            Save = (value) => {
                set(value);
                Owner.SetMcmConfig(key, value);
            },
            Read = () => Owner.GetMcmConfig<bool>(key),
        };
        McmManager.ResetMcmConfig(Owner);
        set(Owner.GetMcmConfig<bool>(key));
        IndexPage.Add(mcmToggle);
        return mcmToggle;
    }
}
