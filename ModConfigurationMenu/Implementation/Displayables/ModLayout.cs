using ChronoArkMod.ModData;
using ModConfigurationMenu.Api.Displayables;

namespace ModConfigurationMenu.Implementation.Displayables;

#nullable enable

internal class ModLayout(IPage InitPage) : IModLayout
{
    private readonly Dictionary<string, IPage> _pages = [];

    public IPage IndexPage => InitPage;
    public IPage? CurrentPage { get; private set; }
    public required ModInfo Owner { get; init; }

    public void Init()
    {
        _pages[SanitizedName("init")] = InitPage;
    }

    public void ChangeToPage(string name)
    {
        name = SanitizedName(name);
        if (_pages.TryGetValue(name, out IPage page)) {
            CurrentPage = page;
            Debug.Log($"changed current page to {name}");
        } else {
            Debug.Log($"can't change current page {name}, it's null");
        }
    }

    public IPage? GetPage(string name)
    {
        name = SanitizedName(name);
        var page = _pages.GetValueOrDefault(name);
        Debug.Log($"retrieved page {name}" + (page is null ? ", but it's null" : ""));
        return page;
    }

    public void AddPage(string name, IPage page)
    {
        name = SanitizedName(name);
        var success = _pages.TryAdd(name, page);
        Debug.Log($"added page {name}, " + (success ? "success" : "duplicate"));
    }

    public void RemovePage(string name)
    {
        name = SanitizedName(name);
        var success = _pages.Remove(name);
        Debug.Log($"removed page {name}, " + (success ? "success" : "nonexist"));
    }

    private string SanitizedName(string name)
    {
        if (string.IsNullOrEmpty(name)) {
            throw new ArgumentNullException(nameof(name));
        }

        var prefix = $"{Owner.id}_";
        if (!name.StartsWith(prefix)) {
            name = prefix + name;
        }
        return name;
    }
}
