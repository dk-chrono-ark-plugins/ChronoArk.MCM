namespace Mcm.Implementation.Displayables;

internal class McmVertical(McmStyle? styleOverride = null)
    : McmComposite(ICompositeLayout.LayoutGroup.Vertical, styleOverride)
{
}