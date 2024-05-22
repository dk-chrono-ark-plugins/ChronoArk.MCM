using ChronoArkMod.ModData;

namespace Mcm.Api;

#nullable enable

/// <summary>
/// Holds a registered mcm mod and its pages
/// </summary>
public interface IModLayout
{
    IPage IndexPage { get; init; }
    ModInfo Owner { get; init; }

    IPage? GetPage(string name);
    IPage AddPage(string name, IPage page);
    void RemovePage(string name);
}
