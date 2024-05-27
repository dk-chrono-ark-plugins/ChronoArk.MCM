using ChronoArkMod.ModData;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

/// <summary>
///     Grid layout page, will return the layout content holder to the render pipe
/// </summary>
internal class McmVerticalPage(ModInfo info) : McmScrollPage(info)
{
    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var content = base.Render(parent);

        var vert = content.AddComponent<VerticalLayoutGroup>();
        vert.spacing = 20f;
        vert.padding = new(20, 20, 20, 20);
        vert.childAlignment = TextAnchor.MiddleCenter;
        vert.childControlWidth = true;
        vert.childControlHeight = true;
        vert.childForceExpandWidth = false;
        vert.childForceExpandHeight = true;

        var contentSizeFitter = content.AddComponent<ContentSizeFitter>();
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

        RenderPageElements(content);

        return content;
    }

    protected override void RenderPageElements(Transform parent)
    {
        foreach (var element in _elements) {
            var grid = element.Render<LayoutElement>(parent);
            var image = grid.GetComponent<Image>();
            if (image != null && image.sprite != null) {
                var imageSize = image.GetComponent<RectTransform>().sizeDelta;
                grid.minWidth = imageSize.x;
                grid.minHeight = imageSize.y;

                grid.preferredWidth = imageSize.x;
                grid.preferredHeight = imageSize.y;
            } else {
                grid.preferredWidth = parent.GetComponent<RectTransform>().rect.width - 40f;
            }
        }
    }
}