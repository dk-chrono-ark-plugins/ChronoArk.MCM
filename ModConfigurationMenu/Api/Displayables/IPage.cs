using ChronoArkMod.ModData;

namespace Mcm.Api.Displayables;

/// <summary>
///     Main displayable used in MCM<br />
///     Responsible for rendering its child elements<br />
///     Instantiated as vertical layout group by default
/// </summary>
public interface IPage : IStylable
{
    /// <summary>
    ///     The page owner info
    /// </summary>
    ModInfo Owner { get; }

    /// <summary>
    ///     Page name, not title
    /// </summary>
    string Name { get; set; }

    /// <summary>
    ///     The page title text, by default the mod name + version.
    /// </summary>
    string Title { get; set; }

    List<IDisplayable> Elements { get; }

    /// <summary>
    ///     Add element to current page.
    /// </summary>
    /// <param name="displayable"><see cref="IDisplayable" /> component</param>
    void Add(IDisplayable displayable);

    /// <summary>
    ///     Remove element from current page.
    /// </summary>
    /// <param name="displayable"><see cref="IDisplayable" /> component</param>
    void Remove(IDisplayable displayable);

    /// <summary>
    ///     Clear all elements of current page.
    /// </summary>
    void Clear();

    /// <summary>
    ///     Add a simple button with only text inside
    /// </summary>
    /// <param name="content">Text</param>
    /// <param name="onClick">Delegate when clicked</param>
    /// <returns></returns>
    IButton AddButton(string content, Action onClick);

    /// <summary>
    ///     Add a button with custom content
    /// </summary>
    /// <param name="content"><see cref="IDisplayable" /> content</param>
    /// <param name="onClick">Delegate when clicked</param>
    /// <returns></returns>
    IButton AddButton(IDisplayable content, Action onClick);

    /// <summary>
    ///     Add a simple image element
    /// </summary>
    /// <param name="color">The mask color to display</param>
    /// <returns><see cref="IImage" /> can be saved for later modification</returns>
    IImage AddImage(Color color);

    /// <summary>
    ///     Add a sprite element
    /// </summary>
    /// <param name="sprite">The sprite to display</param>
    /// <returns><see cref="IImage" /> can be saved for later modification</returns>
    IImage AddImage(Sprite sprite);

    /// <summary>
    ///     Add a sprite element, from file in Assets folder
    /// </summary>
    /// <param name="assetName">file name, with extension</param>
    /// <returns><see cref="IImage" /> can be saved for later modification</returns>
    IImage AddImage(string assetName);


    /// <summary>
    ///     Add text breakline
    /// </summary>
    /// <param name="thickness">Line length, default 5f</param>
    /// <param name="color">Line color, default gray</param>
    /// <returns><see cref="ILine" /> can be saved for later modification</returns>
    ILine AddSeparator(float thickness = 5f, Color? color = null);

    /// <summary>
    ///     Add text element
    /// </summary>
    /// <param name="text">The text to display</param>
    /// <returns><see cref="IText" /> can be saved for later modification</returns>
    IText AddText(string text);

    /// <summary>
    ///     Add text element, localized using I2Loc
    /// </summary>
    /// <param name="locKey">The loc key for the text to display</param>
    /// <returns><see cref="IText" /> can be saved for later modification</returns>
    IText AddTextLoc(string locKey);
}