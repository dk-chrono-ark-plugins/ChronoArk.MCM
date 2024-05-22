using ChronoArkMod.ModData;

namespace Mcm.Implementation;

#nullable enable

internal class ModLayout : IModLayout
{
    private readonly Dictionary<string, IPage> _pages = [];

    public IPage IndexPage { get; init; }
    public ModInfo Owner { get; init; }

    public ModLayout(IPage index)
    {
        Owner = index.Owner;
        index.Name = "index";
        var key = SanitizedName("index");
        _pages[key] = index;
        IndexPage = _pages[key];
    }

    public IPage? GetPage(string name)
    {
        name = SanitizedName(name);
        var page = _pages.GetValueOrDefault(name);
        Debug.Log($"retrieving page {name}" + (page == null ? ", but it's null" : ""));
        return page;
    }

    public IPage AddPage(string name, IPage page)
    {
        page.Name = name;
        var key = SanitizedName(name);
        if (_pages.TryAdd(key, page)) {
            Debug.Log($"{page.Owner.Title} added page {name}");
        }
        return _pages[key];
    }

    public void RemovePage(string name)
    {
        var key = SanitizedName(name);
        var success = _pages.Remove(key);
        Debug.Log($"removed page {key}, " + (success ? "success" : "nonexist"));
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
