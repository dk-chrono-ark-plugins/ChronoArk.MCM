﻿using ChronoArkMod.Helper;
using I2.Loc;
using Mcm.Implementation.Components;
using TMPro;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Mcm.Implementation;

#nullable enable

[HarmonyPatch]
internal static class MainOptionsPatch
{
    public const string ButtonEntryName = "MCM Button";
    public const string ButtonEntryText = "Mods";
    private const string _layoutHierarchy = "Image/Layout";

    private static GameObject? _mcm;

    [HarmonyPostfix]
    [HarmonyPatch(typeof(MainOptionMenu), "Start")]
    private static void OnStart(MainOptionMenu __instance)
    {
        var layout = __instance.transform.GetFirstNestedChildWithName(_layoutHierarchy);
        if (layout == null) {
            return;
        }

        var button = Object.Instantiate(layout.GetChild(0), layout);
        button.name = ButtonEntryName;
        var option = button.GetComponent<OptionButton>();
        var tmp = option.Content.GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault();
        if (tmp == null) {
            Object.DestroyImmediate(button);
            return;
        }
        tmp.text = ButtonEntryText;
        Object.DestroyImmediate(tmp.GetComponent<Localize>());

        var @event = option.gameObject.GetOrAddComponent<EventTrigger>();
        @event.triggers.Remove(@event.triggers.First(t => t.eventID == EventTriggerType.PointerClick));
        @event.AddOrMergeTrigger(EventTriggerType.PointerClick, OnPointerClick);

        _mcm = new(nameof(McmWindow), typeof(RectTransform), typeof(McmWindow));
        _mcm.SetActive(false);
        _mcm.transform.SetParent(__instance.transform.parent);
    }

    private static void OnPointerClick(BaseEventData _)
    {
        _mcm?.GetComponent<McmWindow>()?.Open();
    }
}
