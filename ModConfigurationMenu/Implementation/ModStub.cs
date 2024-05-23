using ChronoArkMod.ModData;
using ChronoArkMod.ModData.Settings;
using Mcm.Api.Configurables;

namespace Mcm.Implementation;

#nullable enable

public static class ModStub
{
    /// <summary>
    /// Get a stub dict from game's version of mod setting entries</br>
    /// </summary>
    /// <param name="modInfo">Owner</param>
    /// <returns>Dict used for <see cref="McmManager.McmRegistry"/></returns>
    public static Dictionary<string, McmSettingEntry> StubMcmConfig(this ModInfo modInfo)
    {
        modInfo.ReadModSetting();
        return modInfo.ModSettingEntries.ToDictionary(
            entry => entry.Key,
            entry => {
                var setting = new McmSettingEntry() {
                    Name = entry.DisplayName,
                    Description = entry.Description,
                };

                if (entry is DropdownSetting) {
                    setting.EntryType = IBasicEntry.EntryType.Dropdown;
                    setting.Value = modInfo.GetSetting<DropdownSetting>(entry.Key).Value;
                } else if (entry is InputFieldSetting) {
                    setting.EntryType = IBasicEntry.EntryType.Input;
                    setting.Value = modInfo.GetSetting<InputFieldSetting>(entry.Key).Value;
                } else if (entry is InputFieldSetting_Int) {
                    setting.EntryType = IBasicEntry.EntryType.Input;
                    setting.Value = modInfo.GetSetting<InputFieldSetting_Int>(entry.Key).Value;
                } else if (entry is SliderSetting) {
                    setting.EntryType = IBasicEntry.EntryType.Slider;
                    setting.Value = modInfo.GetSetting<SliderSetting>(entry.Key).Value;
                } else if (entry is ToggleSetting) {
                    setting.EntryType = IBasicEntry.EntryType.Toggle;
                    setting.Value = modInfo.GetSetting<ToggleSetting>(entry.Key).Value;
                }

                return setting;
            }
        );
    }

    /// <summary>
    /// Generate a stub index page for the mod, with its configs
    /// </summary>
    /// <param name="modInfo"></param>
    public static void StubMcmPage(this ModInfo modInfo)
    {
        var registry = McmManager.GetMcmRegistry(modInfo)
            ?? throw new InvalidOperationException($"{modInfo.id} hasn't been registered or failed with MCM");

        if (modInfo.settings.Count == 0) {
            return;
        }

        Debug.Log("attempt to generate a stub page...");

        var index = registry.Layout.IndexPage;
        index.Clear();

        index.AddText("StubPage");
        index.AddSeparator();

        foreach (var (key, entry) in modInfo.StubMcmConfig()) {
            switch (entry.EntryType) {
                case IBasicEntry.EntryType.Dropdown:
                    break;
                case IBasicEntry.EntryType.Input:
                    break;
                case IBasicEntry.EntryType.Slider:
                    break;
                case IBasicEntry.EntryType.Toggle: {
                    registry.Layout.AddToggleOption(key, entry.Name, entry.Description,
                        get: () => modInfo.GetMcmConfig<bool>(key),
                        set: (value) => modInfo.SetMcmConfig(key, value)
                    );
                    break;
                }
                default:
                    break;
            }
        }
        McmManager.SaveMcmConfig(modInfo);
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
