using ChronoArkMod.ModData;

namespace Mcm.Common;

internal static class McmLoc
{
    internal static string I2Loc(this ModInfo modInfo, string key)
    {
        return modInfo.localizationInfo.SystemLocalizationUpdate(key);
    }

    internal static class Page
    {
        public static readonly string Changed = McmMod.ModInfo.I2Loc("Mcm/Page/Changed");
        public static readonly string RestartPrompt = McmMod.ModInfo.I2Loc("Mcm/Page/RestartPrompt");
        public static readonly string StubPrompt = McmMod.ModInfo.I2Loc("Mcm/Page/StubPrompt");
        public static readonly string Untitled = McmMod.ModInfo.I2Loc("Mcm/Page/Untitled");
    }
}
