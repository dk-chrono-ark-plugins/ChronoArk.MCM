namespace MCM.Common;

internal static class PageSizeFitter
{
    public static Vector2 Max => new(Display.main.renderingWidth, Display.main.renderingHeight);
    public static Vector2 Normal => Max * 0.75f;

    public static Vector2 BorderThickness => new(10f, 10f);
}
