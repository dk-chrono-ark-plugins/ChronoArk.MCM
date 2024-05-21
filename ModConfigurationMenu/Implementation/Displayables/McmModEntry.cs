using ChronoArkMod.ModData;
using Mcm.Implementation.Components;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmModEntry : ScriptRef
{
    public readonly McmButton ModEntry;

    public McmModEntry(ModInfo modInfo)
    {
        var cover = new McmImage() { MainSprite = modInfo.CoverSprite ?? McmWindow.ModUI!.DefaultCover };
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
        ModEntry = new() {
            Content = modEntryInternal,
            OnClick = () => McmWindow.Instance?.RenderIndexPage(modInfo)
        };
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var modEntry = ModEntry.Render<RectTransform>(parent);
        Ref = modEntry.gameObject;

        modEntry.sizeDelta = new(320f, 480f);

        return modEntry;
    }
}
