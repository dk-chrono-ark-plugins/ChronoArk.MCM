using ChronoArkMod.ModData;
using ChronoArkMod.ModData.Settings;
using Mcm.Api.Configurables;

namespace Mcm.Implementation;

#nullable enable

internal static class ModStub
{
    internal static Dictionary<string, McmSettingEntry> StubMcmConfig(this ModInfo modInfo)
    {
        modInfo.ReadModSetting();
        return modInfo.ModSettingEntries
            .ToDictionary(
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
}
