using ChronoArkMod.Helper;
using MCM.Api.Displayables;
using TMPro;

namespace MCM.Implementation.Displayables;

#nullable enable

internal class McmText : ScriptRef, IText
{
    public required string? Content { get; set; }

    public override Transform Render(Transform parent)
    {
        var text = parent.AttachRectTransformObject("McmText");
        var tmp = text.AddComponent<TextMeshProUGUI>();
        tmp.fontSizeMin = 10f;
        tmp.fontSizeMax = 40f;
        tmp.enableAutoSizing = true;
        tmp.autoSizeTextContainer = true;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.text = Content;

        Ref = text.gameObject;
        return text;
    }
}
