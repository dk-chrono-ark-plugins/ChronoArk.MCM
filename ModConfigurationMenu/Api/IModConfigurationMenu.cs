// C# 7.3 ?? NO

// ReSharper disable UnusedMember.Global

namespace Mcm.Api;

/// <summary>
///     Main external API for mods
/// </summary>
public interface IModConfigurationMenu
{
    enum Version
    {
        V1,
    }

    /// <summary>
    ///     Use this to verify against MCM's running version<br />
    ///     MCM is backwards compatible
    /// </summary>
    /// <returns><see cref="Version" /> Version enum</returns>
    Version GetVersion();

    /// <summary>
    ///     Make your mod known to MCM, creates an index page
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    /// <returns>New <see cref="IModLayout" /> to use for adding configs</returns>
    IModLayout Register(string mod);

    /// <summary>
    ///     Unregister from MCM
    /// </summary>
    /// <param name="mod">Your mod, don't use others...</param>
    void Unregister(string mod);
}

public static class McmProxy
{
    public static IModConfigurationMenu GetInstance(IModConfigurationMenu.Version versionMinimum)
    {
        var mcm = McmManager.Instance;
        return mcm.GetVersion() >= versionMinimum
            ? mcm
            : throw new InvalidOperationException($"MCM cannot fulfill the version request!\n" +
                                                  $"Requested {versionMinimum}+, Running {mcm.GetVersion()}");
    }
}