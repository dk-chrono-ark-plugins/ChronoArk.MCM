using System.Globalization;
using Mcm.Api.Configurables;
using Mcm.Implementation.Components;
using Mcm.Implementation.Configurables;
using Mcm.Implementation.Displayables;
using TMPro;

namespace Mcm.Implementation;

internal partial class ModLayout : IModLayout
{
    public IPage AddPage(string name, ICompositeLayout.LayoutGroup layout, bool showAsEntry = false)
    {
        IPage page = layout switch {
            ICompositeLayout.LayoutGroup.Grid => new McmGridPage(Owner),
            ICompositeLayout.LayoutGroup.Vertical => new McmVerticalPage(Owner),
            _ => throw new NotImplementedException(),
        };

        page.Name = name;
        var key = SanitizedName(name);
        if (!_pages.TryAdd(key, page)) {
            return _pages[key];
        }

        Debug.Log($"{page.Owner.Title} added page {name}");
        if (!showAsEntry) {
            return _pages[key];
        }

        McmManager.Instance.ExtraEntries.Add(page);
        Debug.Log("...as separate entry");

        return _pages[key];
    }

    public IPage? GetPage(string name)
    {
        name = SanitizedName(name);
        var page = _pages.GetValueOrDefault(name);
        Debug.Log($"retrieving page {name}" + (page == null ? ", but it's null" : ""));
        return page;
    }

    public void RemovePage(string name)
    {
        if (name == "index") {
            return;
        }

        var key = SanitizedName(name);
        var success = _pages.Remove(key);
        Debug.Log($"removing page {key}, " + (success ? "success" : "nonexistent"));
    }

    public void RenderPage(string name)
    {
        var page = GetPage(name);
        if (page == null) {
            return;
        }

        McmWindow.Instance?.RenderPage(page);
    }

    public void ClosePage(string name)
    {
        var mcm = McmWindow.Instance;
        if (mcm?.TopPage is not null && mcm.TopPage == GetPage(name)) {
            McmWindow.PageBack();
        }
    }

    public void CloseAllPage()
    {
        var mcm = McmWindow.Instance;
        while (mcm?.TopPage is not null && mcm.TopPage.Owner == Owner) {
            McmWindow.PageBack();
        }
    }

    public IPage? CurrentActivePage()
    {
        return McmWindow.Instance?.TopPage;
    }

    public bool IsPageActive(string name)
    {
        return IsPageActive(GetPage(name));
    }

    public bool IsPageActive(IPage? page)
    {
        return page is not null && page == CurrentActivePage();
    }

    public IDropdown AddDropdownMenu(string key, string name, string description,
        Func<string[]> options,
        int @default,
        Action<int> set)
    {
        var entry = MakeEntry(key, name, description, IBasicEntry.EntryType.Dropdown);
        entry.Value = @default;
        McmManager.AddMcmConfig(Owner, key, entry);
        McmManager.ResetMcmConfig(Owner);
        var mcmDropdown = new McmDropdown(key, options, entry) {
            Owner = Owner,
            Save = value => {
                set(value);
                Owner.SetMcmConfig(key, value);
            },
            Read = () => Owner.GetMcmConfig<int>(key),
        };
        set(mcmDropdown.Read());
        IndexPage.Add(mcmDropdown);
        return mcmDropdown;
    }

    public IDropdown AddEnumOption<TE>(string key, string name, string description, 
        TE @default, 
        Action<TE> set) where TE : struct
    {
        var enumValues = Enum.GetValues(typeof(TE)).OfType<TE>().ToList();
        var enumStrings = enumValues.Select(e => e.ToString()).ToList();
        var current = enumStrings.IndexOf(@default.ToString());
        return AddDropdownMenu(key, name, description,
            options: () => enumValues.Select(e => e.ToString()).ToArray(),
            @default: current,
            set: index => {
                Enum.TryParse(enumStrings[index], out TE parsed);
                set(parsed);
            });
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
            McmManager.AddMcmConfig(Owner, key, entry);
            McmManager.ResetMcmConfig(Owner);
            mcmInput = new(key, entry) {
                Owner = Owner,
                Save = value => {
                    set((T)(object)value);
                    Owner.SetMcmConfig(key, value);
                },
                Read = () => Owner.GetMcmConfig<string>(key),
                CharacterValidation = TMP_InputField.CharacterValidation.None,
            };
            set((T)(object)Owner.GetMcmConfig<string>(key));
        } else if (typeof(T) == typeof(int)) {
            entry = MakeEntry(key, name, description, IBasicEntry.EntryType.InputInteger);
            entry.Value = @default;
            McmManager.AddMcmConfig(Owner, key, entry);
            McmManager.ResetMcmConfig(Owner);
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
            set((T)(object)Owner.GetMcmConfig<int>(key));
        } else if (typeof(T) == typeof(float)) {
            entry = MakeEntry(key, name, description, IBasicEntry.EntryType.InputDecimal);
            entry.Value = @default;
            McmManager.AddMcmConfig(Owner, key, entry);
            McmManager.ResetMcmConfig(Owner);
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
            set((T)(object)Owner.GetMcmConfig<float>(key));
        }

        if (mcmInput is null || entry is null) {
            throw new NotImplementedException();
        }
        IndexPage.Add(mcmInput);
        return mcmInput;
    }

    public ISlider AddSliderOption(string key, string name, string description, float min, float max, float step,
        float @default,
        Action<float> set)
    {
        var entry = MakeEntry(key, name, description, IBasicEntry.EntryType.Slider);
        entry.Value = @default;
        McmManager.AddMcmConfig(Owner, key, entry);
        McmManager.ResetMcmConfig(Owner);
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
        set(mcmSlider.Read());
        IndexPage.Add(mcmSlider);
        return mcmSlider;
    }

    public IToggle AddToggleOption(string key, string name, string description,
        bool @default,
        Action<bool> set)
    {
        var entry = MakeEntry(key, name, description, IBasicEntry.EntryType.Toggle);
        entry.Value = @default;
        McmManager.AddMcmConfig(Owner, key, entry);
        McmManager.ResetMcmConfig(Owner);
        var mcmToggle = new McmToggle(key, entry) {
            Owner = Owner,
            Save = value => {
                set(value);
                Owner.SetMcmConfig(key, value);
            },
            Read = () => Owner.GetMcmConfig<bool>(key),
        };
        set(mcmToggle.Read());
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