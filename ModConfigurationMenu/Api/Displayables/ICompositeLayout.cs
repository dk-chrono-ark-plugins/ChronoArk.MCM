namespace Mcm.Api.Displayables;

/// <summary>
///     Composite of several displayables, in a layout group
/// </summary>
public interface ICompositeLayout : IStylable
{
    public enum LayoutGroup
    {
        Grid,
        Horizontal,
        Vertical,
        Overlap,
    }

    Composite[]? Composites { get; }
    LayoutGroup Layout { get; }

    /// <summary>
    ///     Displayable and its preferred size
    /// </summary>
    public sealed record Composite(IDisplayable Displayable, Vector2 Size);
}