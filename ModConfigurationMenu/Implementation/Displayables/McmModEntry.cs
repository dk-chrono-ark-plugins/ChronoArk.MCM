using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmModEntry : ScriptRef
{
    private static Sprite? _defaultCover;
    private static bool _onceFlag;
    private readonly McmButton _modEntry;

    public McmModEntry(ModInfo modInfo)
    {
        LookupOnce();

        var cover = new McmImage() { MainSprite = modInfo.CoverSprite ?? _defaultCover! };
        var text = new McmText() { Content = $"{modInfo.Title}\nv{modInfo.Version}" };
        var bar = new McmImage() {
            MaskColor = Color.blue,
            Stretch = true,
        };
        var modEntryInternal = new McmComposite(ICompositeLayout.LayoutGroup.Vertical) {
            Composites = [
                new(cover, new(0f, 320f)),
                new(text, new(0f, 80f)),
                new(bar, new(0f, 80f)),
            ]
        };
        _modEntry = new() {
            Content = modEntryInternal,
            OnClick = () => ClickEachEntry(modInfo)
        };
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var modEntry = _modEntry.Render<RectTransform>(parent);
        modEntry.sizeDelta = new(320f, 480f);

        Ref = modEntry.gameObject;
        return modEntry;
    }

    private void ClickEachEntry(ModInfo info)
    {
        Debug.Log($"Clicked {info.id}, {info.Author}, {info.Version}");
    }

    private void LookupOnce()
    {
        if (_defaultCover == null && !_onceFlag &&
            ComponentFetch.TryFindObject<Image>("ModeImage", out var image)) {
            _defaultCover = image.sprite;
        }
        _onceFlag = true;
    }
}
