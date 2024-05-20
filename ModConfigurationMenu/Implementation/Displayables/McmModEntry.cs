using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using MCM.Api.Displayables;
using UnityEngine.UI;

namespace MCM.Implementation.Displayables;

#nullable enable

internal class McmModEntry : ScriptRef
{
    private static Sprite? _defaultCover;
    private static bool _onceFlag;
    private readonly McmImage _cover;
    private readonly McmText _text;

    public McmModEntry(ModInfo modInfo)
    {
        LookupOnce();

        _cover = new() { MainSprite = modInfo.CoverSprite ?? _defaultCover! };
        _text = new() { Content = $"{modInfo.Title}\nv{modInfo.Version}" };
    }

    public override Transform Render(Transform parent)
    {
        var modEntry = parent.AttachRectTransformObject("McmModEntry");

        var vert = modEntry.AddComponent<VerticalLayoutGroup>();
        vert.childAlignment = TextAnchor.MiddleCenter;
        vert.childControlHeight = true;
        vert.childForceExpandHeight = false;

        _cover.Render<LayoutElement>(modEntry).preferredHeight = 320f;
        _text.Render<LayoutElement>(modEntry).preferredHeight = 80f;

        var image = new McmImage() { MaskColor = Color.blue };
        image.Render<LayoutElement>(modEntry).preferredHeight = 80f;

        Ref = modEntry.gameObject;
        return modEntry;
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
