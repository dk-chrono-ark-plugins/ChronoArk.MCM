using ChronoArkMod.ModData;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

/// <summary>
///     Grid layout page, will return the grid content holder to the render pipe
/// </summary>
internal class McmGridPage(ModInfo info) : McmScrollPage(info)
{
    private readonly Vector2 _cellSize = new(320f, 400f);

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var content = base.Render(parent);

        var grid = content.AddComponent<GridLayoutGroup>();
        grid.cellSize = _cellSize;
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
        foreach (var grid in _elements.Select(element => element.Render<LayoutElement>(parent))) {
            grid.GetComponent<RectTransform>().SetToStretch();
            grid.preferredWidth = _cellSize.x;
            grid.preferredHeight = _cellSize.y;
        }
    }
}