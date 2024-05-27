namespace Mcm.Implementation.Displayables;

public class McmHorizontal(McmStyle? styleOverride = null)
    : McmComposite(ICompositeLayout.LayoutGroup.Horizontal, styleOverride);