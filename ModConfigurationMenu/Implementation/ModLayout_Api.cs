using System.Globalization;
using Mcm.Api.Configurables;
using Mcm.Implementation.Configurables;
using TMPro;

namespace Mcm.Implementation;

internal partial class ModLayout : IModLayout
{
    public IDropdown AddDropdownMenu(string key, string name, string description,
        Func<string[]> options,
        int @default,
        Action<int> set)
    {
        var entry = MakeEntry(key, name, description, IBasicEntry.EntryType.Dropdown);
        entry.Value = @default;
        var mcmDropdown = new McmDropdown(key, options, entry) {
            Owner = Owner,
            Save = value => {
                set(value);
                Owner.SetMcmConfig(key, value);
            },
            Read = () => Owner.GetMcmConfig<int>(key),
        };
        McmManager.AddMcmConfig(Owner, key, entry);
        IndexPage.Add(mcmDropdown);
        return mcmDropdown;
    }

    public IInputField AddInputField<T>(string key, string name, string description,
        T @default,
        Action<T> set)
    {
        if ((typeof(T) != typeof(string) &&
             typeof(T) != typeof(int) &&
             typeof(T) != typeof(float)) ||
            @default is null) {
            throw new NotImplementedException();
        }

        McmInputField? mcmInput = null;
        McmSettingEntry? entry = null;
        if (typeof(T) == typeof(string)) {
            entry = MakeEntry(key, name, description, IBasicEntry.EntryType.Input);
            entry.Value = @default;
            mcmInput = new(key, entry) {
                Owner = Owner,
                Save = value => {
                    set((T)(object)value);
                    Owner.SetMcmConfig(key, value);
                },
                Read = () => Owner.GetMcmConfig<string>(key),
                CharacterValidation = TMP_InputField.CharacterValidation.None,
            };
        } else if (typeof(T) == typeof(int)) {
            entry = MakeEntry(key, name, description, IBasicEntry.EntryType.InputInteger);
            entry.Value = @default;
            mcmInput = new(key, entry) {
                Owner = Owner,
                Save = value => {
                    if (!int.TryParse(value, out var parsed)) {
                        Debug.Log($"failed to save value {value} as {typeof(int)}");
                        return;
                    }

                    set((T)(object)parsed);
                    Owner.SetMcmConfig(key, parsed);
                },
                Read = () => Owner.GetMcmConfig<int>(key).ToString(),
                CharacterValidation = TMP_InputField.CharacterValidation.Integer,
            };
        } else if (typeof(T) == typeof(float)) {
            entry = MakeEntry(key, name, description, IBasicEntry.EntryType.InputDecimal);
            entry.Value = @default;
            mcmInput = new(key, entry) {
                Owner = Owner,
                Save = value => {
                    if (!float.TryParse(value, out var parsed)) {
                        Debug.Log($"failed to save value {value} as {typeof(float)}");
                        return;
                    }

                    set((T)(object)parsed);
                    Owner.SetMcmConfig(key, parsed);
                },
                Read = () => Owner.GetMcmConfig<float>(key).ToString(CultureInfo.CurrentCulture),
                CharacterValidation = TMP_InputField.CharacterValidation.Decimal,
            };
        }

        if (mcmInput is null || entry is null) {
            throw new NotImplementedException();
        }

        McmManager.AddMcmConfig(Owner, key, entry);
        IndexPage.Add(mcmInput);
        return mcmInput;
    }

    public ISlider AddSliderOption(string key, string name, string description, float min, float max, float step,
        float @default,
        Action<float> set)
    {
        var entry = MakeEntry(key, name, description, IBasicEntry.EntryType.Slider);
        entry.Value = @default;
        var mcmSlider = new McmSlider(key, entry) {
            Owner = Owner,
            Save = value => {
                set(value);
                Owner.SetMcmConfig(key, value);
            },
            Read = () => Owner.GetMcmConfig<float>(key),
            Min = min,
            Max = max,
            Step = step,
        };
        McmManager.AddMcmConfig(Owner, key, entry);
        IndexPage.Add(mcmSlider);
        return mcmSlider;
    }

    public IToggle AddToggleOption(string key, string name, string description,
        bool @default,
        Action<bool> set)
    {
        var entry = MakeEntry(key, name, description, IBasicEntry.EntryType.Toggle);
        entry.Value = @default;
        var mcmToggle = new McmToggle(key, entry) {
            Owner = Owner,
            Save = value => {
                set(value);
                Owner.SetMcmConfig(key, value);
            },
            Read = () => Owner.GetMcmConfig<bool>(key),
        };
        McmManager.AddMcmConfig(Owner, key, entry);
        IndexPage.Add(mcmToggle);
        return mcmToggle;
    }

    private McmSettingEntry MakeEntry(string key, string name, string description, IBasicEntry.EntryType type)
    {
        var entry = new McmSettingEntry(key, Owner.I2Loc(name), Owner.I2Loc(description)) {
            EntryType = type,
        };
        return entry;
    }
}