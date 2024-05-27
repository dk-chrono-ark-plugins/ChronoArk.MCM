using ChronoArkMod.ModData;
using Mcm.Api.Configurables;

namespace Mcm.Api;

/// <summary>
///     Holds a registered mcm mod and its pages
/// </summary>
public interface IModLayout
{
    /// <summary>
    ///     The default page, reserved name is "index"
    /// </summary>
    IPage IndexPage { get; init; }

    /// <summary>
    ///     Page owner
    /// </summary>
    ModInfo Owner { get; init; }

    /// <summary>
    ///     Retrieve a page by name
    /// </summary>
    /// <param name="name">Page name, unique</param>
    /// <returns>Null if page with name dne</returns>
    IPage? GetPage(string name);

    /// <summary>
    ///     Create a page, can be shown as a Mcm main entry
    /// </summary>
    /// <param name="name">Page name, unique</param>
    /// <param name="layout">Page layout</param>
    /// <param name="showAsEntry">Also add this page as Mcm main entry</param>
    /// <returns>Created page or page with same name</returns>
    IPage AddPage(string name, ICompositeLayout.LayoutGroup layout, bool showAsEntry = false);

    /// <summary>
    ///     Remove a page by name
    /// </summary>
    /// <param name="name">Page name, unique</param>
    void RemovePage(string name);

    /// <summary>
    ///     Prepare the page and render it as topmost
    /// </summary>
    /// <remarks>If there's no active Mcm instance in the scene, nothing happens</remarks>
    /// <param name="name">Page name, unique</param>
    void RenderPage(string name);

    /// <summary>
    ///     Add a dropdown selector to the index page
    /// </summary>
    /// <param name="options">Delegate to fetch a list of options</param>
    /// <returns><see cref="IDropdown" /> can be used to change value externally</returns>
    /// <inheritdoc cref="AddToggleOption" />
    IDropdown AddDropdownMenu(string key, string name, string description, Func<string[]> options,
        int @default,
        Action<int> set);

    /// <summary>
    ///     Add an input field to the index page
    /// </summary>
    /// <remarks>Must use <see cref="string" /> or <see cref="int" /> as type parameter</remarks>
    /// <returns><see cref="IInputField" /> can be used to change value externally</returns>
    /// <inheritdoc cref="AddToggleOption" />
    IInputField AddInputField<T>(string key, string name, string description,
        T @default,
        Action<T> set);

    /// <summary>
    ///     Add a slider option to the index page
    /// </summary>
    /// <returns><see cref="ISlider" /> can be used to change value externally</returns>
    /// <inheritdoc cref="AddToggleOption" />
    ISlider AddSliderOption(string key, string name, string description, float min, float max, float step,
        float @default,
        Action<float> set);

    /// <summary>
    ///     Add a toggle option to the index page
    /// </summary>
    /// <param name="key">Unique key of this option</param>
    /// <param name="name">Display name, supports <see cref="I2.Loc" /> keys</param>
    /// <param name="description">Display description, supports <see cref="I2.Loc" /> keys</param>
    /// <param name="set">Delegate to set value</param>
    /// <returns><see cref="IToggle" /> can be used to change state externally</returns>
    IToggle AddToggleOption(string key, string name, string description,
        bool @default,
        Action<bool> set);
}