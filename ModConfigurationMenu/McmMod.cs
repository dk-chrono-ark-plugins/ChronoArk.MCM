global using HarmonyLib;
global using Mcm.Api;
global using Mcm.Api.Displayables;
global using Mcm.Implementation;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using UnityEngine;
using ChronoArkMod.Plugin;

namespace Mcm;

#nullable enable

public class McmMod : ChronoArkPlugin
{
    internal McmConfig? _config;
    internal Harmony? _harmony;
    private static McmMod? _instance;
    private readonly List<IPatch> _patches = [];

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

        _patches.Add(new MainOptionsPatch());

        foreach (IPatch patch in _patches) {
            Debug.Log($"patching {patch.Id}");
            patch.Commit();
            Debug.Log("success!");
        }

        var mcm = McmProxy.GetInstance(IModConfigurationMenu.Version.V1);
        var index = mcm.Register(this);
        mcm.AddText(this, "Displayable here");
        mcm.AddToggleOption(this, "TestToggleKey", "Test Key", "Description", true);

        mcm.AddPage(this, "McmMockup");
        // this is mcm window main entry
        mcm.AddPage(this, "McmEntry", ICompositeLayout.LayoutGroup.Grid);
    }

    private void OnBtnClick()
    {
        Debug.Log("Option setting clicked");
    }
}
