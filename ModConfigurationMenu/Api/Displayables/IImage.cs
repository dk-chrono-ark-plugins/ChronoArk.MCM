namespace MCM.Api.Displayables;

#nullable enable

public interface IImage : IDisplayable
{
    public Color? BorderColor { get; }
    public Vector2? BorderThickness { get; }
    public Color? MaskColor { get; }
    public Sprite? MainSprite { get; }
}
