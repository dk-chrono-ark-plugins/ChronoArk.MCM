using ChronoArkMod.ModData;
using ChronoArkMod.ModData.Settings;
using Mcm.Api.Configurables;

namespace Mcm.Implementation;

public static class ModStub
{
    /// <summary>
    ///     Get a stub dict from game's version of mod setting entries</br>
    /// </summary>
    /// <param name="modInfo">Owner</param>
    /// <returns>Dict used for <see cref="McmManager.McmRegistry" /></returns>
    private static Dictionary<string, McmSettingEntry> StubMcmConfig(this ModInfo modInfo)
    {
        modInfo.ReadModSetting();

        return modInfo.ModSettingEntries.ToDictionary(
            kv => kv.Key,
            kv => {
                var setting = new McmSettingEntry(kv.Key, kv.DisplayName, kv.Description);

                switch (kv) {
                    case DropdownSetting dropdown:
                        setting.EntryType = IBasicEntry.EntryType.Dropdown;
                        setting.Value = dropdown.Value;
                        break;
                    case InputFieldSetting input:
                        setting.EntryType = IBasicEntry.EntryType.Input;
                        setting.Value = input.Value;
                        break;
                    case InputFieldSetting_Int inputInt:
                        setting.EntryType = IBasicEntry.EntryType.Input;
                        setting.Value = inputInt.Value;
                        break;
                    case SliderSetting slider:
                        setting.EntryType = IBasicEntry.EntryType.Slider;
                        setting.Value = slider.Value;
                        setting.Min = slider.MinValue;
                        setting.Max = slider.MaxValue;
                        setting.Step = slider.StepSize;
                        break;
                    case ToggleSetting toggle: {
                        setting.EntryType = IBasicEntry.EntryType.Toggle;
                        setting.Value = toggle.Value;
                        break;
                    }
                }

                return setting;
            });
    }

    /// <summary>
    ///     Generate a stub index page for the mod, with its configs
    /// </summary>
    /// <param name="modInfo"></param>
    public static void StubMcmPage(this ModInfo modInfo)
    {
        var registry = McmManager.GetMcmRegistry(modInfo)
                       ?? throw new InvalidOperationException(
                           $"{modInfo.id} hasn't been registered or failed with MCM");

        if (modInfo.settings.Count == 0) {
            return;
        }

        Debug.Log("attempt to generate a stub page...");

        var index = registry.Layout.IndexPage;

        index.AddText(McmLoc.Page.StubPrompt);
        index.AddSeparator();

        foreach (var (key, entry) in modInfo.StubMcmConfig()) {
            switch (entry.EntryType) {
                case IBasicEntry.EntryType.Dropdown:
                    break;
                case IBasicEntry.EntryType.Input:
                    break;
                case IBasicEntry.EntryType.Slider: {
                    registry.Layout.AddSliderOption(key, entry.Name, entry.Description,
                        entry.Min.GetValueOrDefault(),
                        entry.Max.GetValueOrDefault(),
                        entry.Step.GetValueOrDefault(),
                        _ => { } // already saving in mcm
                    );
                    break;
                }
                case IBasicEntry.EntryType.Toggle: {
                    registry.Layout.AddToggleOption(key, entry.Name, entry.Description,
                        value => modInfo.SetMcmConfig(key, value)
                    );
                    break;
                }
            }
        }
    }

    public static T GetMcmConfig<T>(this ModInfo modInfo, string key)
    {
        return McmManager.GetMcmConfig<T>(modInfo, key);
    }

    public static void SetMcmConfig(this ModInfo modInfo, string key, object value)
    {
        McmManager.UpdateMcmConfig(modInfo, key, value);
    }
}