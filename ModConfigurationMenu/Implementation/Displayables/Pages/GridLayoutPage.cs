using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using UnityEngine.UI;

namespace MCM.Implementation.Displayables;


/// <summary>
/// Grid layout page, will pass the same content holder to the render pipe
/// </summary>
internal sealed class GridLayoutPage(ModInfo Info) : ScrollViewPage(Info)
{
    public Vector2 CellSize = new(320f, 480f);

    public override Transform Render(Transform parent)
    {
        var content = base.Render(parent);

        var gridLayoutGroup = content.AddComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize = CellSize;
        gridLayoutGroup.spacing = new(20f, 20f);
        gridLayoutGroup.padding = new(20, 20, 20, 20);
        gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;

        var contentSizeFitter = content.AddComponent<ContentSizeFitter>();
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

        RenderPageElements(content);

        return content;
    }

    protected override void RenderPageElements(Transform parent)
    {
        foreach (var element in _elements) {
            var grid = element.Render(parent).AddComponent<LayoutElement>();
            grid.preferredWidth = CellSize.x;
            grid.preferredHeight = CellSize.y;
        }
    }
}
