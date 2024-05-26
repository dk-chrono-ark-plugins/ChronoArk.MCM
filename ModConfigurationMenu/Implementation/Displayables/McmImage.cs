using ChronoArkMod.Helper;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

/// <summary>
///     Style: null
/// </summary>
/// <param name="styleOverride"></param>
internal class McmImage(McmStyle? styleOverride = null) : McmStylable(styleOverride), IImage
{
    public Sprite? MainSprite { get; init; }
    public Image? Image { get; private set; }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var image = parent.AttachRectTransformObject("McmImage");

        Image = image.AddComponent<Image>();
        if (Style.Size == null) {
            if (MainSprite != null) {
                Image.sprite = MainSprite;
                Image.SetNativeSize();
            } else {
                image.SetToStretch();
            }
        } else {
            image.sizeDelta = Style.Size.Value;
        }

        DeferredUpdate();

        return base.Render(image);
    }

    public override void Update()
    {
        if (MainSprite != null) {
            Image!.sprite = MainSprite;
        }

        if (Style.OutlineSize != null) {
            Image!.gameObject.GetOrAddComponent<Outline>().effectDistance = Style.OutlineSize.Value;
            if (Style.ColorSecondary != null) {
                Image!.gameObject.GetOrAddComponent<Outline>().effectColor = Style.ColorSecondary.Value;
            }
        }

        if (Style.ColorPrimary != null) {
            Image!.color = Style.ColorPrimary.Value;
        }
    }
}