namespace MCM.Api.Displayables;

#nullable enable

public interface IComposite : IDisplayable
{
    public enum LayoutGroup
    {
        Grid,
        Horizontal,
        Vertical,
    }

    IDisplayable[] Composites { get; }
    LayoutGroup Layout { get; }
    Vector2 PreferredSize { get; }
}
