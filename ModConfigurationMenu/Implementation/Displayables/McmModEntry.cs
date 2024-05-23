using ChronoArkMod.ModData;
using Mcm.Implementation.Components;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmModEntry : ScriptRef
{
    public readonly McmButton ModEntry;
    public override Vector2? Size => new(320f, 640f);
    public ModInfo Owner { get; init; }

    public McmModEntry(ModInfo modInfo, IPage? pageOverride = null, IImage? coverOverride = null, IDisplayable? barOverride = null)
    {
        Owner = modInfo;

        var cover = coverOverride ?? new McmImage() {
            MainSprite = modInfo.CoverSprite ?? McmWindow.ModUI!.DefaultCover
        };
        var text = new McmText() {
            Content = pageOverride?.Title ?? modInfo.Title,
            Size = new(320f, 320f),
        };
        var bar = barOverride ?? new McmImage() {
            MaskColor = Color.blue
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
            OnClick = () => {
                var pageName = pageOverride?.Name ?? "index";
                if (pageName == "index") {
                    Debug.Log($"Loading mod config for {Owner.id}");
                    McmManager.ResetMcmConfig(Owner);
                }
                McmWindow.Instance?.RenderNamedPage(modInfo, pageName);
            },
        };
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var modEntry = ModEntry.Render<RectTransform>(parent);

        modEntry.sizeDelta = Size!.Value;

        return base.Render(modEntry);
    }
}
