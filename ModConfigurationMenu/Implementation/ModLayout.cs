using ChronoArkMod.ModData;

namespace Mcm.Implementation;

#nullable enable

internal class ModLayout : IModLayout
{
    private readonly Dictionary<string, IPage> _pages = [];
    private IPage _currentPage;

    public IPage IndexPage { get; init; }
    public IPage CurrentPage => _currentPage;
    public ModInfo Owner { get; init; }

    public ModLayout(IPage index, ModInfo modInfo)
    {
        Owner = modInfo;
        var name = SanitizedName("index");
        _pages[name] = index;
        IndexPage = _pages[name];
        _currentPage = _pages[name];
    }

    public void ChangeToPage(string name)
    {
        name = SanitizedName(name);
        if (_pages.TryGetValue(name, out IPage page)) {
            _currentPage = page;

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

    public IPage AddPage(string name, IPage page)
    {
        name = SanitizedName(name);
        if (_pages.TryAdd(name, page)) {
            Debug.Log($"{page.Owner.Title} added page {name}");
            _pages[name].Title = name;
        }
        return _pages[name];
    }

    public void RemovePage(string name)
    {
        name = SanitizedName(name);
        if (GetPage(name) == CurrentPage) {
            _currentPage = IndexPage;
        }
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
