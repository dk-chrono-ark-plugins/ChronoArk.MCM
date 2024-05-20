using ChronoArkMod.ModData;
using MCM.Api.Displayables;

namespace MCM.Api;

#nullable enable

internal interface IModLayout
{
    IPage IndexPage { get; init; }
    IPage CurrentPage { get; }
    ModInfo Owner { get; init; }

    void ChangeToPage(string name);
    IPage? GetPage(string name);
    IPage AddPage(string name, IPage page);
    void RemovePage(string name);
}
