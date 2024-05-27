using Mcm.Implementation.Components;
using TMPro;

namespace Mcm.Common;

public sealed record McmStyle
{
    public static Vector2 MaxSize => new(Display.main.renderingWidth, Display.main.renderingHeight);
    public static Vector2? CurrentPageSize => McmWindow.Instance?.TopPage?.Style.Size;

    // common
    public Vector2? Size { get; set; }

    // palette
    public Color? ColorPrimary { get; set; }
    public Color? ColorPrimaryVariant { get; set; }
    public Color? ColorSecondary { get; set; }
    public Color? ColorSecondaryVariant { get; set; }

    // border/outline
    public Vector2? OutlineSize { get; set; }

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

            // color
            ColorPrimary = new(0.17f, 0.28f, 0.49f, 1f),
            ColorPrimaryVariant = new(0.33f, 0.6f, 0.83f, 1f),
            ColorSecondary = new(1f, 1f, 1f, 1f),
            ColorSecondaryVariant = new(0.58f, 0.74f, 0.82f, 1f),

            // border/outline
            OutlineSize = new(10f, 10f),

            // text
            TextAlignment = TextAlignmentOptions.Center,
            TextFontSize = 26f,
            TextAutoSize = false,

            // layout
            LayoutAnchor = TextAnchor.MiddleCenter,
        };
    }

    public McmStyle Copy()
    {
        return this with { };
    }

    public override string ToString()
    {
        return $"Size {Size} OutlineSize {OutlineSize}\n" +
               $"ColorPrimary {ColorPrimary} ColorPrimaryVariant {ColorPrimaryVariant} ColorSecondary {ColorSecondary} ColorSecondaryVariant {ColorSecondaryVariant}\n" +
               $"TextAlignment {TextAlignment} TextFontSize {TextFontSize} TextAutoSize {TextAutoSize}\n" +
               $"LayoutSpacing {LayoutSpacing} LayoutPadding {LayoutPadding} LayoutAnchor {LayoutAnchor}";
    }

    public sealed record SettingLayout
    {
        public static readonly Vector2 NameText = new(400f, 100f);
        public static readonly Vector2 DescText = new(400f, 100f);
        public static readonly Vector2 Setting = new(400f, 100f);
        public static readonly Vector2 SettingSpacingInner = new(10f, 10f);

        public static readonly Vector2 ToggleSingle = new(200f, 100f);
        public static readonly RectOffset TogglePadding = new(5, 5, 10, 10);
        public static readonly Vector2 ToggleSpacing = new(5f, 0f);

        public static readonly RectOffset SliderPadding = new(5, 5, 5, 5);
        public static readonly RectOffset InputPadding = new(2, 2, 10, 10);
    }
}