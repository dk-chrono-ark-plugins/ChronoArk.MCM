using ChronoArkMod.Helper;
using MCM.Api.Displayables;
using MCM.Implementation.Components;
using UnityEngine.UI;

namespace MCM.Implementation.Displayables;

#nullable enable

internal class McmButton : ScriptRef, IButton
{
    private static Animator? _animator;
    private static RuntimeAnimatorController? _aniController;
    private static bool _onceFlag;
    private McmImage _buttonImg;

    public required IDisplayable Content { get; set; }
    public bool Interactable { get; set; }
    public required Action OnClick { get; set; }
    public Vector2? Size { get; set; }

    public McmButton()
    {
        LookUpOnce();

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
        LookUpOnce();

        var button = parent.AttachRectTransformObject("McmButton");
        if (Size == null) {
            button.UseMaxAnchor();
        } else {
            button.sizeDelta = Size.Value;
        }

        var buttonHolder = _buttonImg.Render(button);
        button.AddComponent<ButtonHighlight>().ImageHolder = _buttonImg;
        Content.Render(buttonHolder);

        var component = buttonHolder.AddComponent<Button>();
        component.onClick.AddListener(Click);

        Ref = button.gameObject;
        return buttonHolder;
    }

    private void LookUpOnce()
    {
        if (!_onceFlag && _aniController == null &&
            ComponentFetch.TryFindObject<RuntimeAnimatorController>("OptionButton", out var ani)) {
            _aniController = ani;
        }
        _onceFlag = true;
    }
}
