using ChronoArkMod.ModData;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal partial class McmPage(ModInfo Info) : McmStylable, IPage
{
    protected readonly List<IDisplayable> _elements = [];

    public ModInfo Owner => Info;
    public virtual string Title { get; set; } = McmLoc.Page.Untitled;
    public virtual string Name { get; set; } = "_";
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
