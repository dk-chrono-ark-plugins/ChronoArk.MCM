using ChronoArkMod;
using ChronoArkMod.Plugin;
using Mcm.Api.Configurables;
using Mcm.Implementation.Configurables;
using Mcm.Implementation.Displayables;
using UnityEngine;
using UnityEngine.UI;

namespace Mcm.Implementation;

#nullable enable

internal partial class McmManager : IModConfigurationMenu
{
    // General api

    public IModConfigurationMenu.Version GetVersion()
    {
        return McmInstanceVersion;
    }

    public IPage Register(string mod)
    {
        var modInfo = ModManager.getModInfo(mod);
        var registry = new McmRegistry(new ModLayout(new McmVerticalPage(modInfo), modInfo));
        if (_registries.TryAdd(modInfo, registry)) {
            Debug.Log($"registered {mod}");
            return _registries[modInfo].Layout.IndexPage;
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

    // Config options

    public IToggle AddToggleOption(string mod, string key, string name, string description, bool @default)
    {
        var modInfo = ModManager.getModInfo(mod);
        if (_registries.TryGetValue(modInfo, out var registry)) {
            var nameLoc = modInfo.I2Loc(name);
            var descLoc = modInfo.I2Loc(description);
            var entry = new McmSettingEntry(nameLoc, descLoc, @default, IBasicEntry.EntryType.Toggle);
            if (!registry.Settings.TryAdd(key, entry)) {
                throw new ArgumentException($"key {key} already exist for mod {modInfo.id}");
            }

            var mcmToggle = new McmToggle(key, entry) {
                Owner = modInfo,
                Save = (value) => UpdateModSetting(modInfo, key, value),
                Read = () => GetModSetting<bool>(modInfo, key),
            };
            registry.Layout.IndexPage.Add(mcmToggle);
            return mcmToggle;
        } else {
            throw new InvalidOperationException($"{mod} must be registerd with MCM first");
        }
    }

    // UI elements

    public IText AddText(string mod, string text)
    {
        var modInfo = ModManager.getModInfo(mod);
        if (_registries.TryGetValue(modInfo, out var registry)) {
            var textLoc = modInfo.I2Loc(text);
            var mcmText = new McmText() { Content = textLoc, FontSize = 30f };
            registry.Layout.IndexPage.Add(mcmText);
            return mcmText;
        } else {
            throw new InvalidOperationException($"{mod} must be registerd with MCM first");
        }
    }

    public IImage AddImage(string mod, Sprite sprite)
    {
        var modInfo = ModManager.getModInfo(mod);
        if (_registries.TryGetValue(modInfo, out var registry)) {
            var mcmImage = new McmImage() { MainSprite = sprite };
            registry.Layout.IndexPage.Add(mcmImage);
            return mcmImage;
        } else {
            throw new InvalidOperationException($"{mod} must be registerd with MCM first");
        }
    }

    public IImage AddImage(string mod, Color color)
    {
        var modInfo = ModManager.getModInfo(mod);
        if (_registries.TryGetValue(modInfo, out var registry)) {
            var mcmImage = new McmImage() { MaskColor = color };
            registry.Layout.IndexPage.Add(mcmImage);
            return mcmImage;
        } else {
            throw new InvalidOperationException($"{mod} must be registerd with MCM first");
        }
    }

    public IPage AddPage(string mod, string name, ICompositeLayout.LayoutGroup layout = ICompositeLayout.LayoutGroup.Vertical)
    {
        var modInfo = ModManager.getModInfo(mod);
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
