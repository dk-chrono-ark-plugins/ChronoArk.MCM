using ChronoArkMod.ModData;
using Mcm.Api.Configurables;

namespace Mcm.Api;

#nullable enable

/// <summary>
/// Holds a registered mcm mod and its pages
/// </summary>
public interface IModLayout
{
    /// <summary>
    /// The default page, reserved name is "index"
    /// </summary>
    IPage IndexPage { get; init; }

    /// <summary>
    /// Page owner
    /// </summary>
    ModInfo Owner { get; init; }

    /// <summary>
    /// Retrieve a page by name
    /// </summary>
    /// <param name="name">Page name, unique</param>
    /// <returns>Null if page with name dne</returns>
    IPage? GetPage(string name);

    /// <summary>
    /// Create a page, can be shown as a Mcm main entry
    /// </summary>
    /// <param name="name">Page name, unique</param>
    /// <param name="layout">Page layout</param>
    /// <param name="showAsEntry">Also add this page as Mcm main entry</param>
    /// <returns>Created page or page with same name</returns>
    IPage AddPage(string name, ICompositeLayout.LayoutGroup layout, bool showAsEntry = false);

    /// <summary>
    /// Remove a page by name
    /// </summary>
    /// <param name="name">Page name, unique</param>
    void RemovePage(string name);

    /// <summary>
    /// Prepare the page with name and render it at topmost
    /// </summary>
    /// <remarks>If there's no active Mcm instance in the scene, this will do nothing</remarks>
    /// <param name="name"></param>
    void RenderPage(string name);

    /// <summary>
    /// Add a toggle option to the index page
    /// </summary>
    /// <param name="key">Unique key of this option</param>
    /// <param name="name">Display name, supports <see cref="I2.Loc"/> keys</param>
    /// <param name="description">Display description, supports <see cref="I2.Loc"/> keys</param>
    /// <param name="default">Default value</param>
    IToggle AddToggleOption(string key, string name, string description, bool @default);
}
