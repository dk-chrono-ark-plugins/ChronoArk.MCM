namespace Mcm.Implementation.Displayables;

internal class McmOverlap(McmStyle? styleOverride = null)
    : McmComposite(ICompositeLayout.LayoutGroup.Overlap, styleOverride)
{
    public new IDisplayable[] Composites
    {
        get => base.Composites?.Select(c => c.Displayable).ToArray() ?? [];
        set => base.Composites = value.Select(d => new ICompositeLayout.Composite(d, Style.Size!.Value)).ToArray();
    }
}