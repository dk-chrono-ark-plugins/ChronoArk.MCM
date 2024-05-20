using ChronoArkMod.Helper;
using MCM.Api.Displayables;
using UnityEngine.UI;

namespace MCM.Implementation.Displayables;

#nullable enable

internal class McmComposite : ScriptRef, IComposite
{
    public required IDisplayable[] Composites { get; set; }
    public required IComposite.LayoutGroup Layout { get; set; }
    public required Vector2 PreferredSize { get; set; }

    public override Transform Render(Transform parent)
    {
        var layout = parent.AttachRectTransformObject($"Mcm{Layout}Composite");
        switch (Layout) {
            case IComposite.LayoutGroup.Grid: {
                var group = layout.AddComponent<GridLayoutGroup>();
                group.childAlignment = TextAnchor.MiddleCenter;
                var contentSizeFitter = layout.AddComponent<ContentSizeFitter>();
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                break;
            }
            case IComposite.LayoutGroup.Horizontal: {
                var group = layout.AddComponent<HorizontalLayoutGroup>();
                group.childAlignment = TextAnchor.MiddleCenter;
                break;
            }
            case IComposite.LayoutGroup.Vertical: {
                var group = layout.AddComponent<VerticalLayoutGroup>();
                group.childAlignment = TextAnchor.MiddleCenter;
                break;
            }
        }

        foreach (var displayable in Composites) {
            var element = displayable.Render<LayoutElement>(layout);
            element.preferredWidth = PreferredSize.x;
            element.preferredHeight = PreferredSize.y;
        }

        Ref = layout.gameObject;
        return layout;
    }
}
