using ChronoArkMod.ModData;

namespace ModConfigurationMenu.Api.Displayables;

#nullable enable

public interface IModLayout
{
    IPage IndexPage { get; }
    IPage? CurrentPage { get; }
    ModInfo Owner { get; init; }

    void ChangeToPage(string name);
    IPage? GetPage(string name);
    void AddPage(string name, IPage page);
    void RemovePage(string name);
}
