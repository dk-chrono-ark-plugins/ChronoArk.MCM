using ChronoArkMod;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation;

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
        if (!_registries.TryAdd(modInfo, registry)) {
            throw new InvalidOperationException($"failed to register {mod}");
        }

        ResetMcmConfig(modInfo);

        Debug.Log($"registered {modInfo.id}");
        var layout = _registries[modInfo].Layout;
        if (!modInfo.NeedRestartWhenSettingChanged) {
            return layout;
        }

        layout.IndexPage.AddText(McmLoc.Page.RestartPrompt);
        layout.IndexPage.AddSeparator();

        return layout;
    }

    public void Unregister(string mod)
    {
        var modInfo = ModManager.getModInfo(mod);
        if (_registries.Remove(modInfo)) {
            Debug.Log($"unregistered {mod}");
        }
    }
}