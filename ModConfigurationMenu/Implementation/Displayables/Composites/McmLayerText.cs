namespace Mcm.Implementation.Displayables;

/// <summary>
///     Style: default
/// </summary>
/// <param name="styleOverride"></param>
internal class McmLayerText : McmOverlap
{
    private readonly McmText _text;

    public McmLayerText(IImage layer, McmStyle styleOverride)
        : base(styleOverride)
    {
        Layer = layer;

        _text = new(Style) {
            Content = string.Empty,
        };

        Composites = [layer, _text];
    }

    public string Content
    {
        get => _text.Content;
        set => _text.Content = value;
    }

    public IImage Layer { get; init; }
}