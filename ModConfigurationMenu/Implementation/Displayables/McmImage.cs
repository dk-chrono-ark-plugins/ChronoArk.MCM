using ChronoArkMod.Helper;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmImage : ScriptRef, IImage
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
        if (Size == null) {
            image.SetToStretch();
        } else {
            image.sizeDelta = Size.Value;
        }

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

        return base.Render(image);
    }
}
