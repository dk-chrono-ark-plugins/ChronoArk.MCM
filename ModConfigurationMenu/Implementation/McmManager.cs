using ChronoArkMod;
using ChronoArkMod.ModData;
using ChronoArkMod.Plugin;
using MCM.Api.Displayables;
using MCM.Implementation.Displayables;

namespace MCM.Implementation;

#nullable enable

internal class McmManager : IModConfigurationMenu
{
    public sealed record McmRegistry(IModLayout Layout, Action Save, Action Reset);

    public const IModConfigurationMenu.Version McmInstanceVersion = IModConfigurationMenu.Version.V1;

    private static readonly Lazy<McmManager> _instance = new(() => new());
    private readonly Dictionary<ModInfo, McmRegistry> _registries = [];

    public Dictionary<ModInfo, McmRegistry> Registries => _registries;
    public static McmManager Instance => _instance.Value;

    private McmManager()
    {
    }

    // General api

    public IModConfigurationMenu.Version GetVersion()
    {
        return McmInstanceVersion;
    }

    public IPage Register(ChronoArkPlugin mod, Action apply, Action reset)
    {
        var modInfo = ModManager.getModInfo(mod.ModId);
        if (_registries.TryAdd(modInfo, new(new ModLayout(new GridLayoutPage(modInfo), modInfo), null, null))) {
            Debug.Log($"registered {mod}");
            return _registries[modInfo].Layout.IndexPage;
        } else {
            throw new InvalidOperationException($"failed to register {mod}");
        }
    }

    public void Unregister(ChronoArkPlugin mod)
    {
        var modInfo = ModManager.getModInfo(mod.ModId);
        if (_registries.Remove(modInfo)) {
            Debug.Log($"unregistered {mod}");
        }
    }

    // UI elements

    public void AddText(ChronoArkPlugin mod, Func<string> text)
    {
        throw new NotImplementedException();
    }

    public IPage AddPage(ChronoArkPlugin mod, string name)
    {
        var modInfo = ModManager.getModInfo(mod.ModId);
        if (_registries.TryGetValue(modInfo, out var registry)) {
            return registry.Layout.AddPage(name, new VerticalLayoutPage(modInfo));
        } else {
            throw new InvalidOperationException($"{mod} must be registerd with MCM first");
        }
    }
}
