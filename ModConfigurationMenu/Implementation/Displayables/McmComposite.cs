using ChronoArkMod.Helper;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmComposite(ICompositeLayout.LayoutGroup CompositeLayout) : ScriptRef, ICompositeLayout
{
    public required ICompositeLayout.Composite[] Composites { get; set; }
    public ICompositeLayout.LayoutGroup Layout => CompositeLayout;

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var layout = parent.AttachRectTransformObject($"Mcm{Layout}Composite");
        var contentSizeFitter = layout.AddComponent<ContentSizeFitter>();
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

        switch (Layout) {
            case ICompositeLayout.LayoutGroup.Grid: {
                var group = layout.AddComponent<GridLayoutGroup>();
                group.childAlignment = TextAnchor.MiddleCenter;
                break;
            }
            case ICompositeLayout.LayoutGroup.Horizontal: {
                var group = layout.AddComponent<HorizontalLayoutGroup>();
                group.childAlignment = TextAnchor.MiddleCenter;
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                break;
            }
            case ICompositeLayout.LayoutGroup.Vertical: {
                var group = layout.AddComponent<VerticalLayoutGroup>();
                group.childAlignment = TextAnchor.MiddleCenter;
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                break;
            }
        }

        RenderComposites(layout);

        Ref = layout.gameObject;
        return layout;
    }

    private void RenderComposites(Transform parent)
    {
        foreach (var (displayable, size) in Composites) {
            var element = displayable.Render<LayoutElement>(parent);
            element.GetComponent<RectTransform>().SetToStretch();
            if (!Mathf.Approximately(size.x, 0f)) {
                element.preferredWidth = size.x;
            }
            if (!Mathf.Approximately(size.y, 0f)) {
                element.preferredHeight = size.y;
            }
        }
    }
}
