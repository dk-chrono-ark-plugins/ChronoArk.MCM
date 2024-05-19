global using HarmonyLib;
global using ModConfigurationMenu.Api;
global using ModConfigurationMenu.Implementation;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using UnityEngine;
using ChronoArkMod.Plugin;

namespace ModConfigurationMenu;

#nullable enable

public class ModConfigurationMenuMod : ChronoArkPlugin
{
    private static ModConfigurationMenuMod? _instance;
    private readonly List<IPatch> _patches = [];

    public static ModConfigurationMenuMod? Instance => _instance;
    internal Harmony? _harmony;

    public override void Dispose()
    {
        _instance = null;
    }

    public override void Initialize()
    {
        _instance = this;
        _harmony = new(GetGuid());

        _patches.Add(new MainOptionsPatch());

        foreach (IPatch patch in _patches) {
            if (patch.Mandatory) {
                Debug.Log($"patching {patch.Id}");
                patch.Commit();
                Debug.Log("success!");
            }
        }
    }
}
