using ChronoArkMod.ModData;

namespace Mcm.Common;

public static class I2LocFetch
{
    public static string I2Loc(this ModInfo modInfo, string key)
    {
        return modInfo.localizationInfo.SystemLocalizationUpdate(key);
    }
}
