using ChronoArkMod.Helper;
using ModConfigurationMenu.Api.Displayables;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace ModConfigurationMenu.Implementation.Displayables;

internal class StandardPage(string PageTitle) : IPage
{
    private readonly List<IDisplayable> _elements = [];
    protected bool _dirty;

    public virtual bool Dirty => _dirty;
    public string Title => PageTitle;

    public void Add(IDisplayable displayable)
    {
        _elements.Add(displayable);
        _dirty = true;
    }

    public void Clear()
    {
        _elements.Clear();
        _dirty = true;
    }

    public void Remove(IDisplayable displayable)
    {
        _elements.Remove(displayable);
        _dirty = true;
    }

    public void Render(Transform parent)
    {
        var bg = parent.AttachRectTransformObject("BG");
        bg.sizeDelta = PageSizeFitter.Normal + PageSizeFitter.BorderThickness;
        bg.anchorMin = new(0.5f, 0.5f);
        bg.anchorMax = new(0.5f, 0.5f);
        bg.anchoredPosition = Vector2.zero;
        bg.pivot = new(0.5f, 0.5f);

        var imageBg = bg.gameObject.AddComponent<Image>();
        imageBg.color = Color.white;

        var title = bg.transform.AttachRectTransformObject("Title");
        var titleText = title.gameObject.AddComponent<TextMeshProUGUI>();
        titleText.alignment = TextAlignmentOptions.Center;
        titleText.fontSize = 26f;
        titleText.color = Color.white;
        titleText.text = "Mod Configuration Menu";

        title.anchorMin = new(0.5f, 1f);
        title.anchorMax = new(0.5f, 1f);
        title.pivot = new(0.5f, 0f);
        title.anchoredPosition = new(0f, 20f);
        title.sizeDelta = bg.sizeDelta with { y = 50f };

        var author = bg.transform.AttachRectTransformObject("Author");
        var authorText = title.gameObject.AddComponent<TextMeshProUGUI>();
        authorText.alignment = TextAlignmentOptions.Center;
        authorText.fontSize = 26f;
        authorText.color = Color.white;
        authorText.text = "DK";

        author.anchorMin = new(0.5f, 1f);
        author.anchorMax = new(0.5f, 1f);
        author.pivot = new(0.1f, 0f);
        author.anchoredPosition = new(0f, 20f);
        author.sizeDelta = bg.sizeDelta with { y = 50f };

        var panel = parent.AttachRectTransformObject("MenuPanel");
        panel.sizeDelta = PageSizeFitter.Normal;
        panel.anchorMin = new(0.5f, 0.5f);
        panel.anchorMax = new(0.5f, 0.5f);
        panel.anchoredPosition = Vector2.zero;
        panel.pivot = new(0.5f, 0.5f);

        var imageFg = panel.gameObject.AddComponent<Image>();
        imageFg.color = Color.black;

        var scrollView = new GameObject("ScrollView");
        scrollView.transform.SetParent(panel.transform);
        scrollView.transform.localScale = Vector3.one;
        var scrollViewImage = scrollView.gameObject.AddComponent<Image>();
        scrollViewImage.color = Color.clear;

        var scrollRect = scrollView.gameObject.AddComponent<ScrollRect>();

        var scrollViewRect = scrollView.GetOrAddComponent<RectTransform>();
        panel.sizeDelta = PageSizeFitter.Normal - PageSizeFitter.BorderThickness;
        scrollViewRect.anchorMin = Vector2.zero;
        scrollViewRect.anchorMax = Vector2.one;
        scrollViewRect.offsetMin = Vector2.zero;
        scrollViewRect.offsetMax = Vector2.zero;

        var viewport = scrollView.transform.AttachRectTransformObject("Viewport");
        viewport.gameObject.AddComponent<RectMask2D>();
        var viewportImage = viewport.gameObject.AddComponent<Image>();
        viewportImage.color = Color.clear;
        viewport.anchorMin = Vector2.zero;
        viewport.anchorMax = Vector2.one;
        viewport.offsetMin = Vector2.zero;
        viewport.offsetMax = Vector2.zero;

        scrollRect.viewport = viewport;

        var content = new GameObject("Content");
        content.transform.SetParent(viewport.transform);

        var gridLayoutGroup = content.AddComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize = new(320f, 320f);
        gridLayoutGroup.spacing = new(20f, 20f);
        gridLayoutGroup.padding = new(20, 20, 20, 20);
        gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;

        var contentRect = content.GetComponent<RectTransform>();
        contentRect.anchorMin = Vector2.up;
        contentRect.anchorMax = Vector2.one;
        contentRect.offsetMin = Vector2.zero;
        contentRect.offsetMax = Vector2.zero;
        contentRect.pivot = new(0.5f, 1f);

        var contentSizeFitter = content.AddComponent<ContentSizeFitter>();
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        scrollRect.content = contentRect;

        _dirty = false;
    }
}
