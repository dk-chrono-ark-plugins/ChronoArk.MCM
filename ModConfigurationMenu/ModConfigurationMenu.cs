using ChronoArkMod.Plugin;
using System.Collections.Generic;
using UnityEngine;

namespace ModConfigurationMenu;

#nullable enable

public class MCMMod : ChronoArkPlugin
{
    public static MCMMod? Instance;

    public override void Dispose()
    {
        Instance = null;
    }

    public override void Initialize()
    {
        Instance ??= this;

        var guid = GetGuid();
    }
}
