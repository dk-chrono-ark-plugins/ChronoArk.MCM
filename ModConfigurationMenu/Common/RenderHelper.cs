using UnityEngine.UI;

namespace Mcm.Common;

public static class RenderHelper
{
    public static Canvas Setup(GameObject? current = null)
    {
        if (current == null || current.GetComponent<Canvas>() != null) {
            current = new("McmRenderHelper");
        }

        var canvas = current.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        current.GetOrAddComponent<CanvasScaler>();
        current.GetOrAddComponent<GraphicRaycaster>();
        return canvas;
    }
}