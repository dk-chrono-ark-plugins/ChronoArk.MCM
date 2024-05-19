using ChronoArkMod.Plugin;

namespace ModConfigurationMenu.Implementation;

#nullable enable

internal class McmManager : IModConfigurationMenu
{
    public const int McmInstanceVersion = 1;

    private static readonly Lazy<McmManager> _instance = new(() => new());
    private readonly HashSet<ChronoArkPlugin> _plugins = [];

    public static McmManager Instance => _instance.Value;

    private McmManager()
    {
    }

    // General api

    public int GetVersion()
    {
        return McmInstanceVersion;
    }

    public void Register(ChronoArkPlugin mod, Action apply, Action reset)
    {
        if (_plugins.Add(mod)) {
            Debug.Log($"registered mod {mod.PluginName} @ {mod.ModId}");
        }
    }

    public void Unregister(ChronoArkPlugin mod)
    {
        if (_plugins.Remove(mod)) {
            Debug.Log($"unregistered mod {mod.PluginName}");
        }
    }

    // UI elements

    public void AddPage(ChronoArkPlugin mod, string pageName)
    {
        throw new NotImplementedException();
    }

    public void AddText(ChronoArkPlugin mod, Func<string> text)
    {
        throw new NotImplementedException();
    }
}
