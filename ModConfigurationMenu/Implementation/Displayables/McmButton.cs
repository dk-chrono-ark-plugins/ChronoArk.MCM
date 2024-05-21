using ChronoArkMod.Helper;
using Mcm.Implementation.Components;
using System.Collections;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmButton : ScriptRef, IButton
{
    private readonly McmImage _buttonImg;
    private Button? _button;

    public required IDisplayable Content { get; init; }
    public bool Interactable 
    { 
        get => _button?.interactable ?? false;
        set
        {
            CoroutineHelper.Deferred(
                () => _button!.interactable = value,
                () => _button != null
            );
        }
    }
    public required Action OnClick { get; init; }
    public Vector2? Size { get; init; }

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
        Ref = button.gameObject;

        if (Size == null) {
            button.SetToStretch();
        } else {
            button.sizeDelta = Size.Value;
        }

        var buttonHolder = _buttonImg.Render<RectTransform>(button);
        // button always stretch its content
        buttonHolder.SetToStretch();
        var @delegate = button.AddComponent<ButtonHighlight>();
        @delegate.Button = this;
        var content = Content.Render<RectTransform>(buttonHolder);
        // button always stretch its content
        content.SetToStretch();

        var component = buttonHolder.AddComponent<Button>();
        component.onClick.AddListener(Click);
        _button = component;

        return button;
    }
}
