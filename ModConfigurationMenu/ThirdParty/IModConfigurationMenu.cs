using ChronoArkMod.Plugin;

// C# 7.3

// ReSharper disable UnusedMember.Global

namespace ModConfigurationMenu.Api
{
    // Inspired by spacechase0's GenericModConfigMenu for SDV
    public interface IModConfigurationMenu
    {
        // General api

        /// <summary>
        /// Use this to verify against MCM's running version<br/>
        /// MCM is backwards compatible
        /// </summary>
        /// <returns><see cref="int"/> Version number</returns>
        int GetVersion();

        /// <summary>
        /// Make your mod known to MCM
        /// </summary>
        /// <param name="mod">Your mod, don't use others...</param>
        /// <param name="apply"><see cref="Action"/> when configs apply</param>
        /// <param name="reset"><see cref="Action"/> when configs reset</param>
        void Register(ChronoArkPlugin mod, Action apply, Action reset);

        /// <summary>
        /// Unregister from MCM
        /// </summary>
        /// <param name="mod">Your mod, don't use others...</param>
        void Unregister(ChronoArkPlugin mod);


        // UI elements renders from top to bottom

        /// <summary>
        /// Add a page, note that this isn't the default page
        /// </summary>
        /// <param name="mod">Your mod, don't use others...</param>
        /// <param name="pageName">Name of the page</param>
        void AddPage(ChronoArkPlugin mod, string pageName);

        /// <summary>
        /// Add text at current position
        /// </summary>
        /// <param name="mod">Your mod, don't use others...</param>
        /// <param name="text">The text to display</param>
        void AddText(ChronoArkPlugin mod, Func<string> text);
    }

    public static class McmProxy
    {
        public static IModConfigurationMenu GetInstance(int versionRequired = McmManager.McmInstanceVersion)
        {
            McmManager mcm = McmManager.Instance;
            return mcm != null && mcm.GetVersion() >= versionRequired
                ? mcm
                : throw new InvalidOperationException($"MCM cannot fulfil version request!\n" +
                    "Requested {versionRequired}+, Current {mcm!.GetVersion()}");
        }
    }
}
