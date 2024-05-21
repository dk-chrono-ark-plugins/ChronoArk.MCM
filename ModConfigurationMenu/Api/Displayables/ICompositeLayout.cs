namespace Mcm.Api.Displayables;

#nullable enable

/// <summary>
/// Composite of several displayables, in a layout group
/// </summary>
public interface ICompositeLayout : IDisplayable
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
    }

    Composite[] Composites { get; }
    LayoutGroup Layout { get; }
}
