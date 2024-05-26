using ChronoArkMod.Helper;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

/// <summary>
///     Style: null
/// </summary>
/// <param name="compositeLayout"></param>
/// <param name="styleOverride"></param>
internal class McmComposite(ICompositeLayout.LayoutGroup compositeLayout, McmStyle? styleOverride = null)
    : McmStylable(styleOverride), ICompositeLayout
{
    public ICompositeLayout.Composite[]? Composites { get; set; }
    public ICompositeLayout.LayoutGroup Layout => compositeLayout;

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
                    group.childForceExpandHeight = false;

                    if (Style.LayoutSpacing != null) {
                        group.spacing = Style.LayoutSpacing.Value.x;
                    }

                    contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    break;
                }
                case ICompositeLayout.LayoutGroup.Vertical: {
                    var group = layout.AddComponent<VerticalLayoutGroup>();
                    group.childForceExpandWidth = false;
                    if (Style.LayoutSpacing != null) {
                        group.spacing = Style.LayoutSpacing.Value.y;
                    }

                    contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                    break;
                }
                case ICompositeLayout.LayoutGroup.Overlap:
                default:
                    throw new NotImplementedException();
            }

            layout.GetComponent<LayoutGroup>().childAlignment = Style.LayoutAnchor;
            layout.GetComponent<LayoutGroup>().padding = Style.LayoutPadding ?? new();

            RenderComposites(layout);
        }

        return base.Render(layout);
    }

    private void RenderOverlaps(Transform layout)
    {
        foreach (var (displayable, size) in Composites ?? []) {
            var rect = displayable.Render<RectTransform>(layout);
            rect.SetToStretch();
            rect.sizeDelta = size;
            rect.AlignToCenter();
        }
    }

    private void RenderComposites(Transform layout)
    {
        foreach (var (displayable, size) in Composites ?? []) {
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