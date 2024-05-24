using ChronoArkMod.Helper;
using TMPro;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmText(McmStyle? StyleOverride = null) : McmStylable(StyleOverride ?? McmStyle.Default()), IText
{
    public TextMeshProUGUI? Text { get; private set; }
    private string? _content;

    public required string Content
    {
        get => _content ?? string.Empty;
        set
        {
            _content = value;
            DeferredUpdate();
        }
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var text = parent.AttachRectTransformObject("McmText");
        if (Style.Size == null) {
            text.SetToStretch();
        } else {
            text.sizeDelta = Style.Size.Value;
        }

        Text = text.AddComponent<TextMeshProUGUI>();
        DeferredUpdate();

        return base.Render(text);
    }

    public override void Update()
    {
        Text!.alignment = Style.TextAlignment;

        if (Style.TextAutoSize) {
            Text.fontSizeMin = 10f;
            Text.fontSizeMax = 40f;
            Text.enableAutoSizing = true;
            Text.autoSizeTextContainer = true;
        } else {
            Text.fontSize = Style.TextFontSize;
        }

        if (_content != null) {
            Text.text = _content;
        }
    }
}
