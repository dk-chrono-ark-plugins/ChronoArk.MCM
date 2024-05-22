using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

/// <summary>
/// Grid layout page, will return the layout content holder to the render pipe
/// </summary>
internal class McmVerticalPage(ModInfo Info) : McmScrollPage(Info)
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
        vert.childForceExpandWidth = true;
        vert.childForceExpandHeight = false;

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
            grid.preferredWidth = parent.GetComponent<RectTransform>().rect.width - 40f;
            grid.preferredHeight = grid.GetComponent<RectTransform>().rect.height;
        }
    }
}
