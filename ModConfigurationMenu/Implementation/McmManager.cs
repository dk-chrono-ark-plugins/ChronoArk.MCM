using ChronoArkMod;
using ChronoArkMod.ModData;
using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation;

internal partial class McmManager
{
    public const IModConfigurationMenu.Version McmInstanceVersion = IModConfigurationMenu.Version.V1;

    private static readonly Lazy<McmManager> _instance = new(() => new());
    private static readonly Dictionary<ModInfo, McmRegistry> _registries = [];
    public readonly List<IPage> ExtraEntries = [];

    private McmManager()
    {
    }

    public static McmManager Instance => _instance.Value;

    public List<McmModEntry> PopulateModEntries()
    {
        Debug.Log("populating mcm index pages");

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
                        modInfo.StubMcmPage();
                    }

                    registry = GetMcmRegistry(modInfo)
                               ?? throw new InvalidOperationException(
                                   $"cannot populate mod index page for {modInfo.id}");
                }

                var modEntry = new McmModEntry(modInfo) {
                    ModEntry = {
                        Interactable = registry.Settings.Count > 0,
                    },
                };
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
        return _registries.GetValueOrDefault(modInfo);
    }

    public static void AddMcmConfig(ModInfo modInfo, string key, McmSettingEntry entry)
    {
        if (!_registries.TryGetValue(modInfo, out var registry)) {
            return;
        }

        registry.Settings.TryAdd(key, entry);
    }

    public static T GetMcmConfig<T>(ModInfo modInfo, string key)
    {
        if (!_registries.TryGetValue(modInfo, out var registry) ||
            !registry.Settings.TryGetValue(key, out var entry)) {
            return default!;
        }

        if (entry.Value is T parsed) {
            return parsed;
        }

        try {
            // newtonsoft.json!
            if (typeof(T) == typeof(int)) {
                return (T)(object)Convert.ToInt32(entry.Value);
            }

            if (typeof(T) == typeof(float)) {
                return (T)(object)Convert.ToSingle(entry.Value);
            }

            return (T)entry.Value;
        } catch {
            Debug.Log($"failed to get mcm config of type {typeof(T)}, key {key}");
        }

        return default!;
    }

    public static void UpdateMcmConfig<T>(ModInfo modInfo, IConfigurable<T> configurable)
    {
        UpdateMcmConfig(modInfo, configurable.Id, configurable.Value!);
    }

    /// <summary>
    ///     Update single setting
    /// </summary>
    /// <param name="modInfo"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void UpdateMcmConfig(ModInfo modInfo, string key, object value)
    {
        if (!_registries.TryGetValue(modInfo, out var registry)) {
            return;
        }

        if (registry.Settings.TryGetValue(key, out var settings)) {
            settings.Value = value;
        }

        registry.Dirty = true;
    }

    /// <summary>
    ///     Save all
    /// </summary>
    /// <param name="modInfo"></param>
    public static void SaveMcmConfig(ModInfo modInfo, bool force = false)
    {
        if (!_registries.TryGetValue(modInfo, out var registry)) {
            return;
        }

        if (!registry.Dirty && !force) {
            return;
        }

        // sync to CA configs
        modInfo.settings = registry.Settings
            .ToDictionary(kv => kv.Key, kv => kv.Value.Value);

        ConfigCereal.WriteConfig(modInfo.settings, modInfo.modSettingsPath);
        modInfo.WriteMcmConfig(modInfo.settings);

        modInfo.ReadModSetting();
        modInfo.assemblyInfo.Plugins
            .ForEach(p => p.OnModSettingUpdate());

        registry.Dirty = false;
    }

    /// <summary>
    ///     Reset all mcm config to the state of CA configs(if any)
    /// </summary>
    /// <param name="modInfo"></param>
    public static void ResetMcmConfig(ModInfo modInfo)
    {
        if (!_registries.TryGetValue(modInfo, out var registry)) {
            return;
        }

        var current = modInfo.ReadMcmConfig<Dictionary<string, object>>() ?? [];
        current.Keys
            .Where(registry.Settings.ContainsKey)
            .Do(key => registry.Settings[key].Value = current[key]);

        registry.Dirty = false;
    }

    public sealed record McmRegistry(IModLayout Layout)
    {
        public readonly Dictionary<string, McmSettingEntry> Settings = [];
        public bool Dirty = true;
    }
}