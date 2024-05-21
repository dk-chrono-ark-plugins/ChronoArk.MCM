namespace Mcm.Api.Displayables;

#nullable enable

/// <summary>
/// Image component
/// </summary>
public interface IImage : IDisplayable
{
    public Color? BorderColor { get; }
    public Vector2? BorderThickness { get; }
    public Color? MaskColor { get; }
    public Sprite? MainSprite { get; }
    public bool? Stretch { get; }
}
