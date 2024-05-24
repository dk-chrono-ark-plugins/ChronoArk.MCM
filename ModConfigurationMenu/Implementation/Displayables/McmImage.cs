using ChronoArkMod.Helper;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmImage : McmStylable, IImage
{
    public Color? BorderColor { get; init; }
    public Vector2? BorderThickness { get; init; }
    public Color? MaskColor { get; init; }
    public Sprite? MainSprite { get; init; }
    public Image? Image { get; private set; }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var image = parent.AttachRectTransformObject("McmImage");

        Image = image.AddComponent<Image>();
        if (BorderColor != null) {
            Image.gameObject.GetOrAddComponent<Outline>().effectColor = BorderColor.Value;
        }
        if (BorderThickness != null) {
            Image.gameObject.GetOrAddComponent<Outline>().effectDistance = BorderThickness.Value;
        }
        if (MaskColor != null) {
            Image.color = MaskColor!.Value;
        }
        if (MainSprite != null) {
            Image.sprite = MainSprite;
        }

        if (Size == null) {
            if (MainSprite != null) {
                image.sizeDelta = Image.sprite.rect.size;
            } else {
                image.SetToStretch();
            }
        } else {
            image.sizeDelta = Size.Value;
        }

        return base.Render(image);
    }
}
