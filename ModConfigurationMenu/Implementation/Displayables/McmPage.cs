using ChronoArkMod.ModData;

namespace Mcm.Implementation.Displayables;

/// <summary>
///     Style: null
/// </summary>
/// <param name="info"></param>
public partial class McmPage(ModInfo info) : McmStylable(McmStyle.Default()), IPage
{
    protected readonly List<IDisplayable> _elements = [];

    public ModInfo Owner => info;
    public virtual string Title { get; set; } = McmLoc.Page.Untitled;
    public virtual string Name { get; set; } = string.Empty;
    public List<IDisplayable> Elements => _elements;

    public virtual void Add(IDisplayable displayable)
    {
        _elements.Add(displayable);
    }

    public virtual void Clear()
    {
        _elements.Clear();
    }

    public virtual void Remove(IDisplayable displayable)
    {
        _elements.Remove(displayable);
    }

    // TODO UPDATE CHILD RUNTIME
}