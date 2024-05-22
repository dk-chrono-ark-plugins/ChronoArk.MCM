namespace Mcm.Api.Displayables;

#nullable enable

/// <summary>
/// Image component
/// </summary>
public interface IImage : IDisplayable
{
    public Color? BorderColor { get; set; }
    public Vector2? BorderThickness { get; set; }
    public Color? MaskColor { get; set; }
    public Sprite? MainSprite { get; set; }
}
