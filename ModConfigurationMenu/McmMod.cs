﻿global using HarmonyLib;
global using Mcm.Api;
global using Mcm.Api.Displayables;
global using Mcm.Common;
global using Mcm.Implementation;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using UnityEngine;
global using Debug = Mcm.Common.Debug;
using ChronoArkMod;
using ChronoArkMod.ModData;
using ChronoArkMod.Plugin;

namespace Mcm;

#nullable enable

public class McmMod : ChronoArkPlugin
{
    internal McmConfig? _config;
    internal Harmony? _harmony;
    private static McmMod? _instance;

    public static ModInfo ModInfo => ModManager.getModInfo(Instance!.ModId);
    public static McmMod? Instance => _instance;

    public override void Dispose()
    {
        _instance = null;
    }

    public override void Initialize()
    {
        _instance = this;
        _config = new();
        _harmony = new(GetGuid());
        _harmony.PatchAll();

        var mcm = McmProxy.GetInstance(IModConfigurationMenu.Version.V1);
        var layout = mcm.Register(ModId);

        // mcm window main entry
        layout.AddPage("McmEntry", ICompositeLayout.LayoutGroup.Grid).Title = "Mod Configuration Menu";

        McmMockup.Mockup(layout);
        McmMockup.Stub(layout);
    }
}
