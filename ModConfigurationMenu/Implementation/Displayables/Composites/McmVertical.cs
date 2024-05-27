namespace Mcm.Implementation.Displayables;

public class McmVertical(McmStyle? styleOverride = null)
    : McmComposite(ICompositeLayout.LayoutGroup.Vertical, styleOverride);