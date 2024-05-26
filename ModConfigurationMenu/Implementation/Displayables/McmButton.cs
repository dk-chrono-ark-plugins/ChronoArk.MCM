using ChronoArkMod.Helper;
using Mcm.Implementation.Components;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

/// <summary>
///     Style: default + 3f, 3f border
/// </summary>
internal class McmButton : McmStylable, IButton
{
    private readonly McmImage _buttonImg;
    private Button? _button;
    private bool _interactable = true;

    public McmButton(McmStyle? styleOverride = null)
        : base(styleOverride)
    {
        if (styleOverride == null) {
            Style = McmStyle.Default() with {
                OutlineSize = new(3f, 3f),
            };
        }

        _buttonImg = new(Style);
    }

    public bool DisableGradient { get; init; }
    public required IDisplayable Content { get; init; }

    public bool Interactable
    {
        get => _interactable;
        set
        {
            _interactable = value;
            DeferredUpdate();
        }
    }

    public required Action OnClick { get; init; }

    public void Click()
    {
        OnClick();
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var button = parent.AttachRectTransformObject("McmButton");

        if (Style.Size == null) {
            button.SetToStretch();
        } else {
            button.sizeDelta = Style.Size.Value;
        }

        var buttonHolder = _buttonImg.Render<RectTransform>(button);
        // button always stretch its content
        buttonHolder.SetToStretch();
        if (!DisableGradient) {
            button.AddComponent<ButtonHighlight>().Button = this;
        }

        var content = Content.Render<RectTransform>(buttonHolder);
        // button always stretch its content
        content.SetToStretch();

        _button = buttonHolder.AddComponent<Button>();
        _button.onClick.AddListener(Click);
        DeferredUpdate();

        return base.Render(button);
    }

    public override void Update()
    {
        _button!.interactable = _interactable;
    }
}