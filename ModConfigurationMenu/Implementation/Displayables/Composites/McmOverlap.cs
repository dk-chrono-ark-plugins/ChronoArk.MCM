namespace Mcm.Implementation.Displayables;

public class McmOverlap(McmStyle? styleOverride = null)
    : McmComposite(ICompositeLayout.LayoutGroup.Overlap, styleOverride)
{
    public new List<IDisplayable> Composites
    {
        get => base.Composites?.Select(c => c.Displayable).ToList() ?? [];
        set => base.Composites = value.Select(d => new ICompositeLayout.Composite(d, Style.Size!.Value)).ToList();
    }
}