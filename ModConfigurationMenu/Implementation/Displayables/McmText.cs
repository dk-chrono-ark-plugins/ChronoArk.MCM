using ChronoArkMod.Helper;
using TMPro;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmText : ScriptRef, IText
{
    private TextMeshProUGUI? _text;
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
    public IImage? Bg { get; init; }

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

        _text = text.AddComponent<TextMeshProUGUI>();
        _text.alignment = TextAlignmentOptions.Center;
        DeferredUpdate();

        return base.Render(text);
    }

    public override void DeferredUpdate()
    {
        if (_deferred) {
            return;
        }
        _deferred = true;
        CoroutineHelper.Deferred(
            () => {
                if (_fontsize != null) {
                    _text!.fontSize = _fontsize!.Value;
                } else {
                    _text!.fontSizeMin = 10f;
                    _text.fontSizeMax = 40f;
                    _text.enableAutoSizing = true;
                    _text.autoSizeTextContainer = true;
                }
                if (_content != null) {
                    _text!.text = _content;
                }
                
                _dirty = true;
                _deferred = false;
            },
            () => _text != null
        );
    }
}
