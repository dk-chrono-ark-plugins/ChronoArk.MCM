using ChronoArkMod.ModData;
using Mcm.Implementation.Components;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation;

internal partial class ModLayout
{
    private readonly Dictionary<string, IPage> _pages = [];

    public ModLayout(IPage index)
    {
        Owner = index.Owner;
        index.Name = "index";
        var key = SanitizedName("index");
        _pages[key] = index;
        IndexPage = index;
    }

    public IPage IndexPage { get; init; }
    public ModInfo Owner { get; init; }

    public IPage AddPage(string name, ICompositeLayout.LayoutGroup layout, bool showAsEntry = false)
    {
        IPage page = layout switch {
            ICompositeLayout.LayoutGroup.Grid => new McmGridPage(Owner),
            ICompositeLayout.LayoutGroup.Vertical => new McmVerticalPage(Owner),
            _ => throw new NotImplementedException(),
        };

        page.Name = name;
        var key = SanitizedName(name);
        if (!_pages.TryAdd(key, page)) {
            return _pages[key];
        }

        Debug.Log($"{page.Owner.Title} added page {name}");
        if (!showAsEntry) {
            return _pages[key];
        }

        McmManager.Instance.ExtraEntries.Add(page);
        Debug.Log("...as separate entry");

        return _pages[key];
    }

    public IPage? GetPage(string name)
    {
        name = SanitizedName(name);
        var page = _pages.GetValueOrDefault(name);
        Debug.Log($"retrieving page {name}" + (page == null ? ", but it's null" : ""));
        return page;
    }

    public void RemovePage(string name)
    {
        if (name == "index") {
            return;
        }

        var key = SanitizedName(name);
        var success = _pages.Remove(key);
        Debug.Log($"removing page {key}, " + (success ? "success" : "nonexistent"));
    }

    public void RenderPage(string name)
    {
        var page = GetPage(name);
        if (page == null) {
            return;
        }

        McmWindow.Instance?.RenderPage(page);
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