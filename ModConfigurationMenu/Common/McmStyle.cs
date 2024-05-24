using TMPro;

namespace Mcm.Common;

#nullable enable

public sealed record McmStyle
{
    public static Vector2 MaxSize => new(Display.main.renderingWidth, Display.main.renderingHeight);

    // common
    public Vector2? Size { get; set; }
    public Color? MainColor { get; set; }

    // border/outline
    public Vector2? BorderSize { get; set; }
    public Color? BorderColor { get; set; }

    // text
    public TextAlignmentOptions TextAlignment { get; set; }
    public float TextFontSize { get; set; }
    public bool TextAutoSize { get; set; }

    // layout
    public Vector2? LayoutSpacing { get; set; }
    public RectOffset? LayoutPadding { get; set; }
    public TextAnchor LayoutAnchor { get; set; }


    public static McmStyle Default()
    {
        return new() {
            Size = MaxSize * 0.75f,
            MainColor = new(0.25f, 0.36f, 0.46f, 1f),

            // border/outline
            BorderSize = new(10f, 10f),
            BorderColor = new(0.93f, 0.94f, 0.95f, 1f),

            // text
            TextAlignment = TextAlignmentOptions.Center,
            TextFontSize = 26f,
            TextAutoSize = false,

            // layout
            LayoutAnchor = TextAnchor.MiddleCenter,
        };
    }

    public override string ToString()
    {
        return $"Size {Size} MainColor {MainColor} BorderSize {BorderSize} BorderColor {BorderColor}\n" +
            $"TextAlignment {TextAlignment} TextFontSize {TextFontSize} TextAutoSize {TextAutoSize}\n" +
            $"LayoutSpacing {LayoutSpacing} LayoutPadding {LayoutPadding} LayoutAnchor {LayoutAnchor}";
    }
}
