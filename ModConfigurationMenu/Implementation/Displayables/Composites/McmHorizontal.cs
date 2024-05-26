namespace Mcm.Implementation.Displayables;

internal class McmHorizontal(McmStyle? styleOverride = null)
    : McmComposite(ICompositeLayout.LayoutGroup.Horizontal, styleOverride)
{
}