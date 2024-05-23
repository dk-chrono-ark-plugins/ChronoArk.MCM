using ChronoArkMod.ModData;

namespace Mcm.Common;

internal static class I2LocFetch
{
    internal static string I2Loc(this ModInfo modInfo, string key)
    {
        return modInfo.localizationInfo.SystemLocalizationUpdate(key);
    }
}
