using ChronoArkMod.ModData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcm.Implementation;

internal static class I2LocFetch
{
    internal static string I2Loc(this ModInfo modInfo, string key)
    {
        return modInfo.localizationInfo.SystemLocalizationUpdate(key);
    }
}
