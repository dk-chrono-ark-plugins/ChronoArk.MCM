using ChronoArkMod.ModData;
using Mcm.Implementation.Components;

namespace Mcm.Implementation.Displayables;

/// <summary>
///     Style: fixed
/// </summary>
internal class McmModEntry : McmStylable
{
    public readonly McmButton ModEntry;

    public McmModEntry(ModInfo modInfo, IPage? pageOverride = null, IImage? coverOverride = null)
        : base(McmStyle.Default())
    {
        Owner = modInfo;

        var cover = coverOverride ?? new McmImage {
            MainSprite = modInfo.CoverSprite ?? McmWindow.ModUI!.DefaultCover,
        };

        var textBg = new McmImage(new() { ColorPrimary = Color.black });
        var text = new McmLayerText(textBg, Style with { Size = new(320f, 80f) }) {
            Content = pageOverride?.Title ?? modInfo.Title,
        };

        var modEntryInternal = new McmVertical(Style) {
            Composites = [
                new(cover, new(320f, 320f)),
                new(text, new(320f, 80f)),
            ],
        };
        ModEntry = new() {
            Content = modEntryInternal,
            OnClick = () => ClickPageEntry(pageOverride?.Name ?? "index"),
        };
    }

    public ModInfo Owner { get; }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var modEntry = ModEntry.Render<RectTransform>(parent);
        modEntry.sizeDelta = Style.Size!.Value;

        return base.Render(modEntry);
    }

    private void ClickPageEntry(string name)
    {
        if (name == "index") {
            Debug.Log($"Loading mod config for {Owner.id}");
            McmManager.ResetMcmConfig(Owner);
        }

        McmWindow.Instance?.RenderNamedPage(Owner, name);
    }
}