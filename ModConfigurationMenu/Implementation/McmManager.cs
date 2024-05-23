using ChronoArkMod;
using ChronoArkMod.ModData;
using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;
using static UnityEngine.UI.GridLayoutGroup;

namespace Mcm.Implementation;

#nullable enable

internal partial class McmManager : IModConfigurationMenu
{
    public sealed record McmRegistry(IModLayout Layout)
    {
        public Dictionary<string, McmSettingEntry> Settings = [];
        public bool Dirty = true;
    }

    public const IModConfigurationMenu.Version McmInstanceVersion = IModConfigurationMenu.Version.V1;

    private readonly Dictionary<ModInfo, McmRegistry> _registries = [];
    private static readonly Lazy<McmManager> _instance = new(() => new());

    public List<IPage> ExtraEntries = [];
    public Dictionary<ModInfo, McmRegistry> Registries => _registries;
    public static McmManager Instance => _instance.Value;

    private McmManager()
    {
    }

    public List<McmModEntry> PopulateModEntries()
    {
        Debug.Log($"populating mcm index pages");

        var indexes = ExtraEntries
            .Select(e => new McmModEntry(e.Owner, e))
            .ToList();
        foreach (var modInfo in ModManager.LoadedMods.Select(ModManager.getModInfo)) {
            try {
                var registry = GetMcmRegistry(modInfo);
                if (registry == null) {
                    Register(modInfo.id);
                    if (modInfo.settings.Count > 0) {
                        Debug.Log($"{modInfo.id} has legacy settings and is not registered with MCM");
                        Debug.Log("attempt to generate a stub page...");
                        modInfo.StubMcmPage();
                    }
                    registry = GetMcmRegistry(modInfo)
                        ?? throw new InvalidOperationException($"cannot populate mod index page for {modInfo.id}");
                }
                var modEntry = new McmModEntry(modInfo);
                modEntry.ModEntry.Interactable = registry.Settings.Count > 0;
                indexes.Add(modEntry);
            } catch (Exception ex) {
                Debug.Log($"failed: {ex.Message}");
                // noexcept
            }
        }

        return [.. indexes.OrderByDescending(e => e.ModEntry.Interactable).ThenBy(e => e.Owner.id)];
    }

    public static McmRegistry? GetMcmRegistry(ModInfo modInfo)
    {
        return Instance.Registries.GetValueOrDefault(modInfo);
    }

    public static void AddModSetting(ModInfo modInfo, string key, McmSettingEntry entry)
    {
        if (!Instance.Registries.TryGetValue(modInfo, out var registry)) {
            return;
        }
        registry.Settings.TryAdd(key, entry);
    }

    public static T GetModSetting<T>(ModInfo modInfo, string key)
    {
        if (Instance.Registries.TryGetValue(modInfo, out var registry) &&
            registry.Settings.TryGetValue(key, out var entry) &&
            entry.Value is T parsed) {
            return parsed;
        }
        return default!;
    }

    public static void UpdateModSetting<T>(ModInfo modInfo, IConfigurable<T> configurable)
    {
        UpdateModSetting(modInfo, configurable.Id, configurable.Value!);
    }

    public static void UpdateModSetting(ModInfo modInfo, string key, object value)
    {
        if (!Instance.Registries.TryGetValue(modInfo, out var registry)) {
            return;
        }
        if (registry.Settings.TryGetValue(key, out var settings)) {
            settings.Value = value;
        }
        registry.Dirty = true;
    }

    public static void SaveModSetting(ModInfo modInfo)
    {
        if (!Instance.Registries.TryGetValue(modInfo, out var registry)) {
            return;
        }
        if (registry.Dirty) {
            // force update?
            modInfo.settings = registry.Settings
                .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            ConfigCereal.WriteConfig(modInfo.settings, modInfo.modSettingsPath);
            modInfo.WriteMcmConfig(modInfo.settings);
            modInfo.ReadModSetting();
            modInfo.assemblyInfo.Plugins
                .ForEach(p => p.OnModSettingUpdate());
            registry.Dirty = false;
        }
    }

    public static void ResetModSetting(ModInfo modInfo)
    {
        if (!Instance.Registries.TryGetValue(modInfo, out var registry)) {
            return;
        }
        var current = modInfo.ReadMcmConfig<Dictionary<string, object>>() ?? [];
        current.Keys
           .Where(registry.Settings.ContainsKey)
           .Do(key => registry.Settings[key].Value = current[key]);
        registry.Dirty = false;
    }
}
