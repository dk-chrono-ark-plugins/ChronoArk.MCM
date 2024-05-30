using ChronoArkMod.ModData;
using ChronoArkMod.ModData.Settings;
using Mcm.Api.Configurables;

namespace Mcm.Implementation;

public static class ModStub
{
    /// <summary>
    ///     Get a stub dict from game's version of mod setting entries<br />
    ///     This does not write/modify anything
    /// </summary>
    /// <param name="modInfo">Owner</param>
    /// <returns>Temporary dict used for <see cref="McmManager.McmRegistry" /></returns>
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
                        setting.Options = dropdown.Options.ToArray();
                        break;
                    case InputFieldSetting input:
                        setting.EntryType = IBasicEntry.EntryType.Input;
                        setting.Value = input.Value;
                        break;
                    case InputFieldSetting_Int inputInt:
                        setting.EntryType = IBasicEntry.EntryType.InputInteger;
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
    ///     Generate a stub index page for the mod, with its configs from <see cref="StubMcmConfig" />
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

        var layout = registry.Layout;
        var index = layout.IndexPage;
        index.AddText(McmLoc.Page.StubPrompt);
        index.AddSeparator();

        foreach (var (key, entry) in modInfo.StubMcmConfig()) {
            try {
                switch (entry.EntryType) {
                    case IBasicEntry.EntryType.Dropdown: {
                        layout.AddDropdownMenu(key, entry.Name, entry.Description,
                            () => entry.Options ?? [],
                            (int)entry.Value,
                            _ => { }
                        );
                        break;
                    }
                    case IBasicEntry.EntryType.Input: {
                        layout.AddInputField(key, entry.Name, entry.Description,
                            (string)entry.Value,
                            _ => { }
                        );
                        break;
                    }
                    case IBasicEntry.EntryType.InputDecimal: {
                        layout.AddInputField(key, entry.Name, entry.Description,
                            (float)entry.Value,
                            _ => { }
                        );
                        break;
                    }
                    case IBasicEntry.EntryType.InputInteger: {
                        layout.AddInputField(key, entry.Name, entry.Description,
                            (int)entry.Value,
                            _ => { }
                        );
                        break;
                    }
                    case IBasicEntry.EntryType.Slider: {
                        layout.AddSliderOption(key, entry.Name, entry.Description,
                            entry.Min.GetValueOrDefault(),
                            entry.Max.GetValueOrDefault(),
                            entry.Step.GetValueOrDefault(),
                            (float)entry.Value,
                            _ => { }
                        );
                        break;
                    }
                    case IBasicEntry.EntryType.Toggle: {
                        layout.AddToggleOption(key, entry.Name, entry.Description,
                            (bool)entry.Value,
                            _ => { }
                        );
                        break;
                    }
                    case IBasicEntry.EntryType.Unknown:
                    case IBasicEntry.EntryType.FileBrowser:
                    default:
                        throw new NotImplementedException();
                }
            } catch (Exception ex) {
                Debug.Log($"failed to add setting {key} : {ex.Message}");
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