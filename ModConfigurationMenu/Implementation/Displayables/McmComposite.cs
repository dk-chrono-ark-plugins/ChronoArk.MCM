using ChronoArkMod.Helper;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmComposite(ICompositeLayout.LayoutGroup CompositeLayout, McmStyle? StyleOverride = null) : McmStylable(StyleOverride), ICompositeLayout
{
    public required ICompositeLayout.Composite[] Composites { get; set; }
    public ICompositeLayout.LayoutGroup Layout => CompositeLayout;

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var layout = parent.AttachRectTransformObject($"Mcm{Layout}Composite");

        if (Style.Size == null) {
            layout.SetToStretch();
        } else {
            layout.sizeDelta = Style.Size.Value;
        }

        if (Layout == ICompositeLayout.LayoutGroup.Overlap) {
            RenderOverlaps(layout);
        } else {
            var contentSizeFitter = layout.AddComponent<ContentSizeFitter>();
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

            switch (Layout) {
                case ICompositeLayout.LayoutGroup.Grid: {
                    var group = layout.AddComponent<GridLayoutGroup>();
                    if (Style.LayoutSpacing != null) {
                        group.spacing = Style.LayoutSpacing.Value;
                    }
                    break;
                }
                case ICompositeLayout.LayoutGroup.Horizontal: {
                    var group = layout.AddComponent<HorizontalLayoutGroup>();
                    if (Style.LayoutSpacing != null) {
                        group.spacing = Style.LayoutSpacing.Value.x;
                    }
                    contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    break;
                }
                case ICompositeLayout.LayoutGroup.Vertical: {
                    var group = layout.AddComponent<VerticalLayoutGroup>();
                    if (Style.LayoutSpacing != null) {
                        group.spacing = Style.LayoutSpacing.Value.y;
                    }
                    contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                    break;
                }
            }

            layout.GetComponent<LayoutGroup>().childAlignment = Style.LayoutAnchor;
            layout.GetComponent<LayoutGroup>().padding = Style.LayoutPadding ?? new();

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
