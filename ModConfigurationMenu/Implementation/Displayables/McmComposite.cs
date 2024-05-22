using ChronoArkMod.Helper;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmComposite(ICompositeLayout.LayoutGroup CompositeLayout) : ScriptRef, ICompositeLayout
{
    public required ICompositeLayout.Composite[] Composites { get; set; }
    public ICompositeLayout.LayoutGroup Layout => CompositeLayout;
    public Vector2? Spacing { get; init; }
    public RectOffset? Padding { get; init; }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var layout = parent.AttachRectTransformObject($"Mcm{Layout}Composite");

        if (Size == null) {
            layout.SetToStretch();
        } else {
            layout.sizeDelta = Size.Value;
        }

        var contentSizeFitter = layout.AddComponent<ContentSizeFitter>();
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

        switch (Layout) {
            case ICompositeLayout.LayoutGroup.Grid: {
                var group = layout.AddComponent<GridLayoutGroup>();
                group.childAlignment = TextAnchor.MiddleCenter;
                if (Spacing != null) {
                    group.spacing = Spacing.Value;
                }
                if (Padding != null) {
                    group.padding = Padding;
                }
                break;
            }
            case ICompositeLayout.LayoutGroup.Horizontal: {
                var group = layout.AddComponent<HorizontalLayoutGroup>();
                group.childAlignment = TextAnchor.MiddleCenter;
                if (Spacing != null) {
                    group.spacing = Spacing.Value.x;
                }
                if (Padding != null) {
                    group.padding = Padding;
                }
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                break;
            }
            case ICompositeLayout.LayoutGroup.Vertical: {
                var group = layout.AddComponent<VerticalLayoutGroup>();
                group.childAlignment = TextAnchor.MiddleCenter;
                if (Spacing != null) {
                    group.spacing = Spacing.Value.y;
                }
                if (Padding != null) {
                    group.padding = Padding;
                }
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                break;
            }
        }

        if (Layout == ICompositeLayout.LayoutGroup.Overlap) {
            UnityEngine.Object.DestroyImmediate(contentSizeFitter);
            RenderOverlaps(layout);
        } else {
            RenderComposites(layout);
        }

        return base.Render(layout);
    }

    private void RenderOverlaps(Transform layout)
    {
        foreach (var (displayable, size) in Composites) {
            var rect = displayable.Render<RectTransform>(layout);
            rect.SetToStretch();
            rect.sizeDelta = size;
            rect.AlignToCenter();
        }
    }

    private void RenderComposites(Transform layout)
    {
        foreach (var (displayable, size) in Composites) {
            var element = displayable.Render<LayoutElement>(layout);
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
