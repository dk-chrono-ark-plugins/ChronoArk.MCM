namespace ModConfigurationMenu.Api.Displayables;

public interface IPage : IDisplayable
{
    bool Dirty { get; }
    string Title { get; }

    void Add(IDisplayable displayable);
    void Remove(IDisplayable displayable);
    void Clear();
}
