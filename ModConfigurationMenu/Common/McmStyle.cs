namespace Mcm.Common;

#nullable enable

public sealed record McmStyle
{
    public static readonly Vector2 MaxSize = new(Display.main.renderingWidth, Display.main.renderingHeight);
    
    public Vector2 Size { get; set; }
    public Vector2 BorderSize { get; set; }

    public Color MaskColor { get; set; }
    public Color BorderColor { get; set; }



    public static readonly McmStyle Default = new() {
        Size = MaxSize * 0.75f,
        BorderSize = new(10f, 10f),

        MaskColor = new(0.25f, 0.36f, 0.46f, 1f),
        BorderColor = new(0.93f, 0.94f, 0.95f, 1f),

    };
}
