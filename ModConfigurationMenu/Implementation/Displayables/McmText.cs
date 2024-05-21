using ChronoArkMod.Helper;
using TMPro;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmText : ScriptRef, IText
{
    public required string? Content { get; set; }
    public float? FixedSize { get; set; }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var text = parent.AttachRectTransformObject("McmText");
        var tmp = text.AddComponent<TextMeshProUGUI>();
        if (FixedSize is not null) {
            tmp.fontSize = FixedSize.Value;
        } else {
            tmp.fontSizeMin = 10f;
            tmp.fontSizeMax = 40f;
            tmp.enableAutoSizing = true;
            tmp.autoSizeTextContainer = true;
        }
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.text = Content;

        Ref = text.gameObject;
        return text;
    }
}
