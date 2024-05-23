using ChronoArkMod;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation;

#nullable enable

internal partial class McmManager : IModConfigurationMenu
{
    public IModConfigurationMenu.Version GetVersion()
    {
        return McmInstanceVersion;
    }

    public IModLayout Register(string mod)
    {
        var modInfo = ModManager.getModInfo(mod);
        var registry = new McmRegistry(new ModLayout(new McmVerticalPage(modInfo)));
        if (_registries.TryAdd(modInfo, registry)) {
            Debug.Log($"registered {mod}");
            var layout = _registries[modInfo].Layout;
            if (modInfo.NeedRestartWhenSettingChanged) {
                layout.IndexPage.AddText(McmLoc.Page.RestartPrompt);
                layout.IndexPage.AddSeparator();
            }
            return layout;
        } else {
            throw new InvalidOperationException($"failed to register {mod}");
        }
    }

    public void Unregister(string mod)
    {
        var modInfo = ModManager.getModInfo(mod);
        if (_registries.Remove(modInfo)) {
            Debug.Log($"unregistered {mod}");
        }
    }
}
