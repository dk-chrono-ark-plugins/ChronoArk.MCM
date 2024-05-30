using Mcm.Api.Configurables;

namespace Mcm.Api.Versions;

public interface IModLayoutV2
{
    /// <summary>
    ///     Close a page, if it's being rendered
    /// </summary>
    /// <remarks>Added in <see cref="IModConfigurationMenu.Version.V2" /></remarks>
    void ClosePage(string name);

    /// <summary>
    ///     Close all currently open page
    /// </summary>
    /// <remarks>Added in <see cref="IModConfigurationMenu.Version.V2" /></remarks>
    void CloseAllPage();

    /// <summary>
    ///     Get current active page if any
    /// </summary>
    /// <returns><see cref="IPage" /> or null</returns>
    /// <remarks>Added in <see cref="IModConfigurationMenu.Version.V2" /></remarks>
    IPage? CurrentActivePage();

    /// <summary>
    ///     Check against the active page if any
    /// </summary>
    /// <param name="name">Page name, unique</param>
    /// <remarks>Added in <see cref="IModConfigurationMenu.Version.V2" /></remarks>
    /// <returns>true if active</returns>
    bool IsPageActive(string name);

    /// <param name="page">Page instance</param>
    /// <inheritdoc cref="IsPageActive(string name)" />
    /// <remarks>Added in <see cref="IModConfigurationMenu.Version.V2" /></remarks>
    bool IsPageActive(IPage page);

    /// <summary>
    ///     Add a dropdown menu for an enum table
    /// </summary>
    /// <typeparam name="TE">
    ///     <see cref="Enum" />
    /// </typeparam>
    /// <inheritdoc cref="IModLayout.AddDropdownMenu" />
    /// <remarks>Added in <see cref="IModConfigurationMenu.Version.V2" /></remarks>
    IDropdown AddEnumOption<TE>(string key, string name, string description,
        TE @default,
        Action<TE> set) where TE : struct;
}