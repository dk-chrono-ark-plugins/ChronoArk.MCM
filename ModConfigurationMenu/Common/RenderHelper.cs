﻿using ChronoArkMod.Helper;
using UnityEngine.UI;

namespace Mcm.Common;

#nullable enable

internal static class RenderHelper
{
    internal static Canvas Setup(GameObject? current = null)
    {
        if (current == null || current.GetComponent<Canvas>() != null) {
            current = new GameObject("McmRenderHelper");
        }
        var canvas = current.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        current.GetOrAddComponent<CanvasScaler>();
        current.GetOrAddComponent<GraphicRaycaster>();
        return canvas;
    }
}