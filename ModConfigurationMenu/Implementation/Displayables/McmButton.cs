using ChronoArkMod.Helper;
using Mcm.Implementation.Components;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmButton : ScriptRef, IButton
{
    private readonly McmImage _buttonImg;

    public required IDisplayable Content { get; set; }
    public bool Interactable { get; set; }
    public required Action OnClick { get; set; }
    public Vector2? Size { get; set; }

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

        button.AddComponent<ButtonHighlight>().ImageHolder = _buttonImg;
        var content = Content.Render<RectTransform>(buttonHolder);
        // button always stretch its content
        content.SetToStretch();

        var component = buttonHolder.AddComponent<Button>();
        component.onClick.AddListener(Click);

        Ref = button.gameObject;
        return button;
    }
}
