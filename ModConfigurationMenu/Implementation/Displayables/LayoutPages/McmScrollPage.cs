using ChronoArkMod.ModData;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

/// <summary>
///     Scroll rect page, will return a content holder to the render pipe
/// </summary>
public class McmScrollPage(ModInfo info) : McmPanel(info)
{
    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        parent = base.Render(parent);

        var scrollView = parent.AttachRectTransformObject("ScrollView", false);
        scrollView.sizeDelta = Style.Size!.Value - Style.OutlineSize!.Value;
        scrollView.SetToStretch();
        scrollView.localScale = Vector3.one;

        var scrollViewImage = scrollView.AddComponent<Image>();
        scrollViewImage.color = Color.clear;

        var scrollRect = scrollView.AddComponent<ScrollRect>();
        scrollRect.horizontal = false;

        var viewport = scrollView.AttachRectTransformObject("Viewport");
        viewport.SetToStretch();
        viewport.AddComponent<RectMask2D>();
        scrollRect.viewport = viewport;

        var viewportImage = viewport.AddComponent<Image>();
        viewportImage.color = Color.clear;

        var scrollbar = scrollView.AttachRectTransformObject("Scrollbar", false);

        var scrollbarComponent = scrollbar.AddComponent<Scrollbar>();
        scrollbarComponent.direction = Scrollbar.Direction.BottomToTop;

        var scrollbarImage = scrollbar.AddComponent<Image>();
        scrollbarImage.color = Style.ColorPrimaryVariant!.Value;

        var slidingArea = scrollbar.AttachRectTransformObject("Slider", false);
        slidingArea.anchorMin = new(0.05f, 0.05f);
        slidingArea.anchorMax = new(0.95f, 0.95f);
        slidingArea.sizeDelta = Vector2.zero;
        slidingArea.pivot = new(0.5f, 0.5f);

        var handle = slidingArea.AttachRectTransformObject("SliderHandle", false);
        handle.anchorMin = Vector2.zero;
        handle.anchorMax = Vector2.zero;
        handle.pivot = new(0.5f, 0.5f);
        handle.sizeDelta = new(5f, 5f);

        var handleImage = handle.AddComponent<Image>();
        handleImage.color = Color.gray;
        scrollbarComponent.targetGraphic = handleImage;
        scrollbarComponent.handleRect = handle;

        scrollbar.anchorMin = new(1f, 0f);
        scrollbar.anchorMax = Vector2.one;
        scrollbar.pivot = new(1f, 0.5f);
        scrollbar.offsetMin = new(-8f, 0f);
        scrollbar.offsetMax = Vector2.zero;
        scrollbar.sizeDelta = new(8f, 0f);

        scrollRect.verticalScrollbar = scrollbarComponent;
        scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;

        var content = viewport.AttachRectTransformObject("Content");
        scrollRect.content = content;

        content.anchorMin = Vector2.up;
        content.anchorMax = Vector2.one;
        content.offsetMin = Vector2.zero;
        content.offsetMax = Vector2.zero;
        content.pivot = new(0.5f, 1f);

        return content;
    }
}