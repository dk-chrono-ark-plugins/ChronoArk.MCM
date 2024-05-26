namespace Mcm.Implementation.Displayables;

/// <summary>
///     Style: null
/// </summary>
/// <param name="styleOverride"></param>
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