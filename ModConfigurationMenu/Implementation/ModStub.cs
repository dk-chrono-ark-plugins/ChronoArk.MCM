using ChronoArkMod.ModData;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod.Plugin;
using Mcm.Api.Configurables;
using Microsoft.Win32;

namespace Mcm.Implementation;

#nullable enable

internal static class ModStub
{
    /// <summary>
    /// Get a stub dict from game's version of mod setting entries</br>
    /// </summary>
    /// <param name="modInfo">Owner</param>
    /// <returns>Dict used for <see cref="McmManager.McmRegistry"/></returns>
    internal static Dictionary<string, McmSettingEntry> StubMcmConfig(this ModInfo modInfo)
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
    /// Generate a stub index page for the mod, regarding its configs</br>
    /// Will clear index page beforehands
    /// </summary>
    /// <param name="modInfo"></param>
    internal static void StubMcmPage(this ModInfo modInfo)
    {
        var registry = McmManager.GetMcmRegistry(modInfo) 
            ?? throw new InvalidOperationException($"{modInfo.id} hasn't been registered or failed with MCM");
        
        if (modInfo.settings.Count == 0) {
            return;
        }

        var index = registry.Layout.IndexPage;
        index.Clear();
        var text = McmManager.Instance.AddText(modInfo.id, "");

        foreach (var (key, entry) in modInfo.StubMcmConfig()) {
            switch (entry.EntryType) {
                case IBasicEntry.EntryType.Patch:
                    break;
                case IBasicEntry.EntryType.Dropdown:
                    break;
                case IBasicEntry.EntryType.Input:
                    break;
                case IBasicEntry.EntryType.Slider:
                    break;
                case IBasicEntry.EntryType.Toggle:{
                    McmManager.Instance.AddToggleOption(modInfo.id, key, entry.Name, entry.Description, false);
                    break;
                }
                default: 
                    break;
            }
        }
        McmManager.SaveModSetting(modInfo);
    }
}
