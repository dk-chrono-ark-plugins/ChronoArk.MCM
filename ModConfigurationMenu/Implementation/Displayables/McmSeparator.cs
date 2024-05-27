using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

/// <summary>
///     Style: Color.gray + 5f thickness
/// </summary>
public class McmSeparator : McmStylable, ILine
{
    public McmSeparator(McmStyle? styleOverride = null)
    {
        Style = styleOverride ?? new() {
            ColorPrimary = Color.gray,
            Size = new(0f, 5f),
        };
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var line = parent.AttachRectTransformObject("McmSeparator");
        line.SetToStretch();

        line.AddComponent<Image>();
        DeferredUpdate();

        return base.Render(line);
    }

    public override void Update()
    {
        Ref!.GetComponent<Image>().color = Style.ColorPrimary!.Value;
        Ref!.GetComponent<LayoutElement>().preferredHeight = Style.Size!.Value.y;
    }
}