using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using MCM.Common;
using UnityEngine.UI;

namespace MCM.Implementation.Displayables;

/// <summary>
/// Scroll rect page, will pass a content holder to the render pipe
/// </summary>
internal class ScrollViewPage(ModInfo Info) : McmPage(Info)
{
    public override Transform Render(Transform parent)
    {
        parent = base.Render(parent);

        var scrollView = parent.AttachRectTransformObject("ScrollView", false);
        scrollView.sizeDelta = PageSizeFitter.Normal - PageSizeFitter.BorderThickness;
        scrollView.UseMaxAnchor();
        scrollView.localScale = Vector3.one;

        var scrollViewImage = scrollView.AddComponent<Image>();
        scrollViewImage.color = Color.clear;

        var scrollRect = scrollView.AddComponent<ScrollRect>();
        scrollRect.horizontal = false;

        var viewport = scrollView.AttachRectTransformObject("Viewport");
        viewport.UseMaxAnchor();
        viewport.AddComponent<RectMask2D>();
        scrollRect.viewport = viewport;

        var viewportImage = viewport.AddComponent<Image>();
        viewportImage.color = Color.clear;

        var scrollbar = scrollView.AttachRectTransformObject("Scrollbar", false);
        scrollbar.sizeDelta = new(5f, 0f);

        var scrollbarComponent = scrollbar.AddComponent<Scrollbar>();
        scrollbarComponent.direction = Scrollbar.Direction.BottomToTop;

        var scrollbarImage = scrollbar.AddComponent<Image>();
        scrollbarImage.StartCoroutine(scrollbarImage.GradientColor());

        var slidingArea = scrollbar.AttachRectTransformObject("Slider", false);
        slidingArea.anchorMin = Vector2.zero;
        slidingArea.anchorMax = new(0.95f, 0.95f);
        slidingArea.sizeDelta = Vector2.zero;
        slidingArea.pivot = new(0.5f, 0.5f);

        var handle = slidingArea.AttachRectTransformObject("SliderHandle", false);
        handle.anchorMin = Vector2.zero;
        handle.anchorMax = Vector2.one;
        handle.pivot = new(0.5f, 0.5f);
        handle.sizeDelta = new(5f, 5f);

        var handleImage = handle.AddComponent<Image>();
        handleImage.color = Color.gray;
        scrollbarComponent.targetGraphic = handleImage;
        scrollbarComponent.handleRect = handle;

        scrollbar.anchorMin = new(1f, 0f);
        scrollbar.anchorMax = new(1f, 1f);
        scrollbar.pivot = new(1f, 0.5f);
        scrollbar.offsetMin = new(-20f, 0f);
        scrollbar.offsetMax = Vector2.zero;
        scrollbar.sizeDelta = new(20f, 0f);

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
