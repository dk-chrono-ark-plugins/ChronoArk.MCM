using ChronoArkMod.Helper;
using Mcm.Implementation.Components;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmButton : ScriptRef, IButton
{
    private bool _interactable = true;
    private readonly McmImage _buttonImg;
    public Button? _button;

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
    public bool DisableGradient { get; init; }
    public IImage Background => _buttonImg;

    public McmButton()
    {
        _buttonImg = new() {
            BorderColor = Color.white,
            BorderThickness = new(3f, 3f),
            MaskColor = Color.black,
        };
    }

    public void Click()
    {
        OnClick?.Invoke();
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var button = parent.AttachRectTransformObject("McmButton");

        if (Size == null) {
            button.SetToStretch();
        } else {
            button.sizeDelta = Size.Value;
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

    public override void DeferredUpdate()
    {
        if (_deferred) {
            return;
        }
        _deferred = true;
        CoroutineHelper.Deferred(
            () => {
                _button!.interactable = _interactable;
                _dirty = true;
                _deferred = false;
            },
            () => _button != null
        );
    }
}
