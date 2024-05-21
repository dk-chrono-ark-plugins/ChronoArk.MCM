using ChronoArkMod.Helper;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmImage : ScriptRef, IImage
{
    public Color? BorderColor { get; set; }
    public Vector2? BorderThickness { get; set; }
    public Color? MaskColor { get; set; }
    public Sprite? MainSprite { get; set; }
    public bool? Stretch { get; set; }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var image = parent.AttachRectTransformObject("McmImage");
        Ref = image.gameObject;

        if (Stretch.GetValueOrDefault()) {
            image.SetToStretch();
        }

        var component = image.AddComponent<Image>();
        if (MainSprite != null) {
            component.sprite = MainSprite;
        } else if (MaskColor != null) {
            component.color = MaskColor.Value;
        }

        if (BorderColor != null) {
            image.gameObject.GetOrAddComponent<Outline>().effectColor = BorderColor.Value;
        }
        if (BorderThickness != null) {
            image.gameObject.GetOrAddComponent<Outline>().effectDistance = BorderThickness.Value;
        }

        return image;
    }
}
