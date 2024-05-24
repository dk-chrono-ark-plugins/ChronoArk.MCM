namespace Mcm.Api.Displayables;

#nullable enable

/// <summary>
/// Composite of several displayables, in a layout group
/// </summary>
public interface ICompositeLayout : IStylable
{
    /// <summary>
    /// Displayable and its preferred size
    /// </summary>
    public sealed record Composite(IDisplayable Displayable, Vector2 Size);

    public enum LayoutGroup
    {
        Grid,
        Horizontal,
        Vertical,
        Overlap,
    }

    Composite[] Composites { get; }
    LayoutGroup Layout { get; }
}
