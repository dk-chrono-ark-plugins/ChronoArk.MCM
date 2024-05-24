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
        public static readonly string RestartPrompt = McmMod.ModInfo.I2Loc("Mcm/Page/RestartPrompt");
        public static readonly string StubPrompt = McmMod.ModInfo.I2Loc("Mcm/Page/StubPrompt");
        public static readonly string Untitled = McmMod.ModInfo.I2Loc("Mcm/Page/Untitled");
        public static readonly string Back = McmMod.ModInfo.I2Loc("Mcm/Page/Back");
        public static readonly string Apply = McmMod.ModInfo.I2Loc("Mcm/Page/Apply");
        public static readonly string Reset = McmMod.ModInfo.I2Loc("Mcm/Page/Reset");
    }

    internal static class Setting
    {
        public static readonly string Changed = McmMod.ModInfo.I2Loc("Mcm/Setting/Changed");
        public static readonly string Placeholder = McmMod.ModInfo.I2Loc("Mcm/Setting/Placeholder");
        public static readonly string ToggleOn = McmMod.ModInfo.I2Loc("Mcm/Setting/ToggleOn");
        public static readonly string ToggleOff = McmMod.ModInfo.I2Loc("Mcm/Setting/ToggleOff");

    }
}
