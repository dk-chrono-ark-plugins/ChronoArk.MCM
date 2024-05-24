using ChronoArkMod.ModData;
using Mcm.Implementation.Components;

namespace Mcm.Implementation.Displayables;

internal class McmModEntry : McmStylable
{
    public readonly McmButton ModEntry;

    public McmModEntry(ModInfo modInfo, IPage? pageOverride = null, IImage? coverOverride = null)
        : base(McmStyle.Default())
    {
        Owner = modInfo;
        Style.Size = new(320f, 400f);

        var cover = coverOverride ?? new McmImage {
            MainSprite = modInfo.CoverSprite ?? McmWindow.ModUI!.DefaultCover,
        };

        var text = new McmComposite(ICompositeLayout.LayoutGroup.Overlap, Style) {
            Composites = [
                new(new McmImage(new() { ColorPrimary = Color.black }),
                    new(320f, 80f)),
                new(new McmText(Style) {
                        Content = pageOverride?.Title ?? modInfo.Title,
                    },
                    new(320f, 80f)),
            ],
        };
        var modEntryInternal = new McmComposite(ICompositeLayout.LayoutGroup.Vertical, Style) {
            Composites = [
                new(cover, new(0f, 320f)),
                new(text, new(0f, 80f)),
            ],
        };
        ModEntry = new() {
            Content = modEntryInternal,
            OnClick = () => InitPageEntry(pageOverride?.Name ?? "index"),
        };
    }

    public ModInfo Owner { get; init; }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var modEntry = ModEntry.Render<RectTransform>(parent);

        modEntry.sizeDelta = Style.Size!.Value;
        (ModEntry.Background as McmImage)!.Image!.color = Color.black;

        return base.Render(modEntry);
    }

    private void InitPageEntry(string name)
    {
        if (name == "index") {
            Debug.Log($"Loading mod config for {Owner.id}");
            McmManager.ResetMcmConfig(Owner);
        }

        McmWindow.Instance?.RenderNamedPage(Owner, name);
    }
}