global using HarmonyLib;
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

public class McmMod : ChronoArkPlugin
{
    internal McmConfig? _config;
    private Harmony? _harmony;

    public static ModInfo? ModInfo => ModManager.getModInfo(Instance!.ModId);
    public static McmMod? Instance { get; private set; }

    public override void Dispose()
    {
        Instance = null;
    }

    public override void Initialize()
    {
        Instance = this;
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