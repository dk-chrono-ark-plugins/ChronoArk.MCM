using ChronoArkMod;
using ChronoArkMod.ModData;
using ChronoArkMod.Plugin;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation;

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

    public static McmRegistry? GetMcmRegistry(ModInfo modInfo)
    {
        return Instance.Registries.GetValueOrDefault(modInfo);
    }

    // General api

    public IModConfigurationMenu.Version GetVersion()
    {
        return McmInstanceVersion;
    }

    public IPage Register(ChronoArkPlugin mod, Action apply, Action reset)
    {
        var modInfo = ModManager.getModInfo(mod.ModId);
        if (_registries.TryAdd(modInfo, new(new ModLayout(new McmGridPage(modInfo), modInfo), apply, reset))) {
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
        
    }

    public IPage AddPage(ChronoArkPlugin mod, string name, ICompositeLayout.LayoutGroup layout = ICompositeLayout.LayoutGroup.Vertical)
    {
        var modInfo = ModManager.getModInfo(mod.ModId);
        if (_registries.TryGetValue(modInfo, out var registry)) {
            return registry.Layout.AddPage(name, layout switch {
                ICompositeLayout.LayoutGroup.Grid => new McmGridPage(modInfo),
                ICompositeLayout.LayoutGroup.Horizontal => new McmGridPage(modInfo),
                ICompositeLayout.LayoutGroup.Vertical => new McmVerticalPage(modInfo),
                _ => throw new NotImplementedException()
            });
        } else {
            throw new InvalidOperationException($"{mod} must be registerd with MCM first");
        }
    }
}
