using ChronoArkMod.ModData;

namespace Mcm.Api.Displayables;

#nullable enable

/// <summary>
/// Main displayable used in MCM<br/>
/// Responsible for rendering its child elements<br/>
/// Instantiated as vertical layout group by default
/// </summary>
public interface IPage : IDisplayable
{
    /// <summary>
    /// The page owner info
    /// </summary>
    ModInfo Owner { get; init; }

    /// <summary>
    /// The page title text, by default the mod name + version.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Add element to current page.
    /// </summary>
    /// <param name="displayable"><see cref="IDisplayable"/> component</param>
    void Add(IDisplayable displayable);

    /// <summary>
    /// Remove element from current page.
    /// </summary>
    /// <param name="displayable"><see cref="IDisplayable"/> component</param>
    void Remove(IDisplayable displayable);

    /// <summary>
    /// Clear all elements of current page.
    /// </summary>
    /// <param name="displayable"><see cref="IDisplayable"/> component</param>
    void Clear();
}
