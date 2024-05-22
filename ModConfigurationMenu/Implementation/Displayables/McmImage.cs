using ChronoArkMod.Helper;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmImage : ScriptRef, IImage
{
    private Image? _image;
    private Color? _borderColor;
    private Vector2? _borderThickness;
    private Color? _maskColor;
    private Sprite? _sprite;

    public Color? BorderColor
    {
        get => _borderColor;
        set
        {
            _borderColor = value;
            DeferredUpdate();
        }
    }
    public Vector2? BorderThickness
    {
        get => _borderThickness;
        set
        {
            _borderThickness = value;
            DeferredUpdate();
        }
    }
    public Color? MaskColor
    {
        get => _maskColor;
        set
        {
            _maskColor = value;
            DeferredUpdate();
        }
    }
    public Sprite? MainSprite
    {
        get => _sprite;
        set
        {
            _sprite = value;
            DeferredUpdate();
        }
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var image = parent.AttachRectTransformObject("McmImage");
        if (Size == null) {
            image.SetToStretch();
        } else {
            image.sizeDelta = Size.Value;
        }

        _image = image.AddComponent<Image>();
        DeferredUpdate();

        return base.Render(image);
    }

    public override void DeferredUpdate()
    {
        if (_deferred) {
            return;
        }
        _deferred = true;
        CoroutineHelper.Deferred(
            () => {
                if (_borderColor != null) {
                    _image!.gameObject.GetOrAddComponent<Outline>().effectColor = _borderColor.Value;
                }
                if (_borderThickness != null) {
                    _image!.gameObject.GetOrAddComponent<Outline>().effectDistance = _borderThickness.Value;
                }
                if (_maskColor != null) {
                    _image!.color = _maskColor!.Value;
                }
                if (_sprite != null) {
                    _image!.sprite = _sprite;
                }
                _dirty = true;
                _deferred = false;
            },
            () => _image != null
        );
    }
}
