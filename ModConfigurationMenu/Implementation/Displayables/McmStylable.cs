namespace Mcm.Implementation.Displayables;

internal class McmStylable(McmStyle? styleOverride = null) : McmDisplayable, IStylable
{
    private McmStyle _style = styleOverride == null ? new() : styleOverride.Copy();

    public McmStyle Style
    {
        get => _style;
        set
        {
            _style = value;
            DeferredUpdate();
        }
    }
}