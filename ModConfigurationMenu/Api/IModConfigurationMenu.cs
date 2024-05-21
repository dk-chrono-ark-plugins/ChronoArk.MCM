using ChronoArkMod.Plugin;

// C# 7.3 ?? NO

// ReSharper disable UnusedMember.Global

namespace Mcm.Api;

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
    /// <param name="apply"><see cref="Action"/> when configs apply</param>
    /// <param name="reset"><see cref="Action"/> when configs reset</param>
    /// <returns>Index <see cref="IPage"/></returns>
    IPage Register(ChronoArkPlugin mod, Action apply, Action reset);

    /// <summary>
    /// Unregister from MCM
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    void Unregister(ChronoArkPlugin mod);

    // UI elements renders from top to bottom

    /// <summary>
    /// Add text element to index page
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    /// <param name="text">The text to display</param>
    void AddText(ChronoArkPlugin mod, Func<string> text);

    // Pages

    /// <summary>
    /// Add a page, note that this isn't the index page
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    /// <param name="name">Name of the page, don't use index</param>
    /// <param name="layout">Page layout, default to vertical</param>
    /// <returns>New <see cref="IPage"/> to use for custom rendering</returns>
    IPage AddPage(ChronoArkPlugin mod, string name, ICompositeLayout.LayoutGroup layout = ICompositeLayout.LayoutGroup.Vertical);
}

public static class McmProxy
{
    public static IModConfigurationMenu GetInstance(IModConfigurationMenu.Version versionRequired = McmManager.McmInstanceVersion)
    {
        var mcm = McmManager.Instance;
        return mcm != null && mcm.GetVersion() >= versionRequired
            ? mcm
            : throw new InvalidOperationException($"MCM cannot fulfill the version request!\n" +
                $"Requested {versionRequired}+, Running {mcm?.GetVersion()}");
    }
}
