using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

/// <summary>
/// Grid layout page, will return the grid content holder to the render pipe
/// </summary>
internal class McmGridPage(ModInfo Info) : McmScrollPage(Info)
{
    public Vector2 CellSize = new(320f, 480f);

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var content = base.Render(parent);

        var grid = content.AddComponent<GridLayoutGroup>();
        grid.cellSize = CellSize;
        grid.spacing = new(20f, 20f);
        grid.padding = new(20, 20, 20, 20);
        grid.childAlignment = TextAnchor.MiddleCenter;

        var contentSizeFitter = content.AddComponent<ContentSizeFitter>();
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

        RenderPageElements(content);

        return content;
    }

    protected override void RenderPageElements(Transform parent)
    {
        foreach (var element in _elements) {
            var grid = element.Render<LayoutElement>(parent);
            grid.GetComponent<RectTransform>().SetToStretch();
            grid.preferredWidth = CellSize.x;
            grid.preferredHeight = CellSize.y;
        }
    }
}
