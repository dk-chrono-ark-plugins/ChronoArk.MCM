namespace Mcm.Common;

#nullable enable

internal static class PageStyle
{
    public static Vector2 Max => new(Display.main.renderingWidth, Display.main.renderingHeight);
    public static Vector2 Normal => Max * 0.75f;

    public static Vector2 BorderThickness => new(10f, 10f);

    public static Color BackColor { get; set; } = new(0.25f, 0.36f, 0.46f, 1f);
    public static Color BorderColor { get; set; } = new(0.93f, 0.94f, 0.95f, 1f);
}
