using ChronoArkMod.Helper;
using Mcm.Implementation.Components;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmButton : ScriptRef, IButton
{
    public Button? Button;
    private bool _interactable = true;
    private readonly McmImage _buttonImg;

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
            BorderColor = PageStyle.BorderColor,
            BorderThickness = new(3f, 3f),
            MaskColor = PageStyle.BackColor,
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

        Button = buttonHolder.AddComponent<Button>();
        Button.onClick.AddListener(Click);
        DeferredUpdate();

        return base.Render(button);
    }

    public override void Update()
    {
        Button!.interactable = _interactable;
    }
}
