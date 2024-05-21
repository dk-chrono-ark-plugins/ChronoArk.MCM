using ChronoArkMod.Helper;
using Mcm.Implementation.Components;
using System.Collections;
using TMPro;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmText : ScriptRef, IText
{
    private TextMeshProUGUI? _text;

    public required string Content
    {
        get => _text?.text ?? string.Empty;
        set
        {
            CoroutineHelper.Deferred(
                () => _text!.text = value,
                () => _text != null
            );
        }
    }
    public float? FixedSize { get; set; }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var text = parent.AttachRectTransformObject("McmText");
        Ref = text.gameObject;

        var tmp = text.AddComponent<TextMeshProUGUI>();
        if (FixedSize != null) {
            tmp.fontSize = FixedSize.Value;
        } else {
            tmp.fontSizeMin = 10f;
            tmp.fontSizeMax = 40f;
            tmp.enableAutoSizing = true;
            tmp.autoSizeTextContainer = true;
        }
        tmp.alignment = TextAlignmentOptions.Center;
        _text = tmp;

        return text;
    }
}
