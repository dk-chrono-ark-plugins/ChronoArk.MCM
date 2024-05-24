using ChronoArkMod.Helper;
using TMPro;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmText : McmStylable, IText
{
    public TextMeshProUGUI? Text { get; private set; }
    private string? _content;
    private float? _fontsize;

    public required string Content
    {
        get => _content ?? string.Empty;
        set
        {
            _content = value;
            DeferredUpdate();
        }
    }
    public float? FontSize
    {
        get => _fontsize;
        set
        {
            _fontsize = value;
            DeferredUpdate();
        }
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var text = parent.AttachRectTransformObject("McmText");

        if (Size == null) {
            text.SetToStretch();
        } else {
            text.sizeDelta = Size.Value;
        }

        Text = text.AddComponent<TextMeshProUGUI>();
        Text.alignment = TextAlignmentOptions.Center;
        DeferredUpdate();

        return base.Render(text);
    }

    public override void Update()
    {
        if (_fontsize != null) {
            Text!.fontSize = _fontsize!.Value;
        } else {
            Text!.fontSizeMin = 10f;
            Text.fontSizeMax = 40f;
            Text.enableAutoSizing = true;
            Text.autoSizeTextContainer = true;
        }
        if (_content != null) {
            Text!.text = _content;
        }
    }
}
