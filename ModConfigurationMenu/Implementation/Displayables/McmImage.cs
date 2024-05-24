using ChronoArkMod.Helper;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmImage(McmStyle? StyleOverride = null) : McmStylable(StyleOverride), IImage
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
        base.Update();

        if (MainSprite != null) {
            Image!.sprite = MainSprite;
        }
        if (Style.BorderColor != null) {
            Image!.gameObject.GetOrAddComponent<Outline>().effectColor = Style.BorderColor.Value;
        }
        if (Style.BorderSize != null) {
            Image!.gameObject.GetOrAddComponent<Outline>().effectDistance = Style.BorderSize.Value;
        }
        if (Style.MainColor != null) {
            Image!.color = Style.MainColor.Value;
        }
    }
}
