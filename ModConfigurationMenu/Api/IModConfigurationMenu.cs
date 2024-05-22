using ChronoArkMod.Plugin;
using Mcm.Api.Configurables;
using UnityEngine.UI;

// C# 7.3 ?? NO

// ReSharper disable UnusedMember.Global

namespace Mcm.Api;

#nullable enable

public interface IModConfigurationMenu
{
    enum Version
    {
        V1,
    }

    // General api

    /// <summary>
    /// Use this to verify against MCM's running version<br/>
    /// MCM is backwards compatible
    /// </summary>
    /// <returns><see cref="Version"/> Version enum</returns>
    Version GetVersion();

    /// <summary>
    /// Make your mod known to MCM, creates an index page
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    /// <returns>Index <see cref="IPage"/><br/>
    /// Use it to add custom displayables
    /// </returns>
    IPage Register(ChronoArkPlugin mod);

    /// <summary>
    /// Unregister from MCM
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    void Unregister(ChronoArkPlugin mod);

    // Config options

    /// <summary>
    /// Add a toggle option to the index page
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    /// <param name="key">Unique key of this option</param>
    /// <param name="name">Display name, supports <see cref="I2.Loc"/> keys</param>
    /// <param name="description">Display description, supports <see cref="I2.Loc"/> keys</param>
    /// <param name="default">Default value</param>
    /// <returns><see cref="IToggle"/> can be used to change state externally</returns>
    IToggle AddToggleOption(ChronoArkPlugin mod, string key, string name, string description, bool @default);



    // Below are APIs for custom page rendering

    // Pages

    /// <summary>
    /// Add a page, note that this isn't the index page
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    /// <param name="name">Name of the page, don't use "index"</param>
    /// <param name="layout">Page layout, default to vertical</param>
    /// <returns>New <see cref="IPage"/> to use for custom rendering</returns>
    IPage AddPage(ChronoArkPlugin mod, string name, ICompositeLayout.LayoutGroup layout = ICompositeLayout.LayoutGroup.Vertical);

    // UI elements renders from top to bottom

    /// <summary>
    /// Add text element to index page
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    /// <param name="text">The text to display</param>
    /// <returns><see cref="IText"/> can be saved for later modification</returns>
    IText AddText(ChronoArkPlugin mod, string text);

    /// <summary>
    /// Add a sprite to index page
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    /// <param name="sprite">The sprite to display</param>
    /// <returns><see cref="IImage"/> can be saved for later modification</returns>
    IImage AddImage(ChronoArkPlugin mod, Sprite sprite);

    /// <summary>
    /// Add a simple image to index page
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    /// <param name="color">The mask color to display</param>
    /// <returns><see cref="IImage"/> can be saved for later modification</returns>
    IImage AddImage(ChronoArkPlugin mod, Color color);
}

public static class McmProxy
{
    public static IModConfigurationMenu GetInstance(IModConfigurationMenu.Version versionRequired)
    {
        var mcm = McmManager.Instance;
        return mcm != null && mcm.GetVersion() >= versionRequired
            ? mcm
            : throw new InvalidOperationException($"MCM cannot fulfill the version request!\n" +
                $"Requested {versionRequired}+, Running {mcm?.GetVersion()}");
    }
}
