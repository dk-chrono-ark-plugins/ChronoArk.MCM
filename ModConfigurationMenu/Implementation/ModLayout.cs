using ChronoArkMod.ModData;

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