using ChronoArkMod.ModData;

namespace Mcm.Implementation;

#nullable enable

internal class ModLayout : IModLayout
{
    private readonly Dictionary<string, IPage> _pages = [];

    public IPage IndexPage { get; init; }
    public ModInfo Owner { get; init; }

    public ModLayout(IPage index, ModInfo modInfo)
    {
        Owner = modInfo;
        var name = SanitizedName("index");
        _pages[name] = index;
        IndexPage = _pages[name];
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
        name = SanitizedName(name);
        if (_pages.TryAdd(name, page)) {
            Debug.Log($"{page.Owner.Title} added page {name}");
        }
        return _pages[name];
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
