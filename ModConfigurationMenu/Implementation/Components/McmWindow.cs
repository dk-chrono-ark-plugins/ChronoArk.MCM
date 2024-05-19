using ChronoArkMod;
using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModConfigurationMenu.Implementation.Components;

#nullable enable

internal class McmWindow : UIBehaviour
{
    public const string ButtonEntryName = "MCM Button";
    public const string ButtonEntryText = "Mods";

    private bool _shouldReturn = true; 

    private Vector2 _borderThickness = new(10f, 10f);
    private Canvas? _canvas;

    private Vector2 MenuSize => new Vector2(Display.main.renderingWidth, Display.main.renderingHeight) * 0.75f;

    private void Start()
    {
        _canvas = gameObject.GetOrAddComponent<Canvas>();
        _canvas.gameObject.GetOrAddComponent<CanvasScaler>();
        _canvas.gameObject.GetOrAddComponent<GraphicRaycaster>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        RenderPage();
    }

    public void RenderPage()
    {
        var bg = _canvas!.transform.AttachRectTransformObject("BG");
        bg.sizeDelta = MenuSize + _borderThickness;
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
        var authorText = author.gameObject.AddComponent<TextMeshProUGUI>();
        authorText.alignment = TextAlignmentOptions.Center;
        authorText.fontSize = 26f;
        authorText.color = Color.white;
        authorText.text = "DK";

        author.anchorMin = new(0.5f, 1f);
        author.anchorMax = new(0.5f, 1f);
        author.pivot = new(0.1f, 0f);
        author.anchoredPosition = new(0f, 20f);
        author.sizeDelta = bg.sizeDelta with { y = 50f };

        var panel = _canvas.transform.AttachRectTransformObject("MenuPanel");
        panel.sizeDelta = MenuSize;
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
        panel.sizeDelta = MenuSize - _borderThickness;
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

        var defaultCover = GameObject.Find("ModeImage")?.GetComponent<Image>();
        foreach (var mod in ModManager.LoadedMods.Select(ModManager.getModInfo)) {
            if (mod != null) {
                var child = new GameObject(mod.id);
                child.transform.SetParent(content.transform);

                var childImage = child.AddComponent<Image>();
                if (mod.CoverSprite == null) {
                    Debug.Log($"null for mod {mod.id}");
                } else {
                    childImage.sprite = mod.CoverSprite;
                }

                var childRect = child.GetOrAddComponent<RectTransform>();
                childRect.sizeDelta = new(320f, 320f);
                childRect.localScale = Vector3.one;
            }
        }
        /**
        for (int i = 0; i < 30; i++) {
            var child = new GameObject("Item " + i);
            child.transform.SetParent(content.transform);
            var childImage = child.AddComponent<Image>();
            childImage.color = Color.white;

            var childRect = child.GetOrAddComponent<RectTransform>();
            childRect.sizeDelta = new(320f, 320f);
            childRect.localScale = Vector3.one;
        }
        /**/
    }

    public void Open()
    {
        var mom = GameObject.Find("MainOptions")?.GetComponent<MainOptionMenu>();
        if (mom !=null) {
            mom.gameObject.SetActive(false);
            gameObject.SetActive(true);
            _shouldReturn = false;
            this.StartDeferredCoroutine(
                () => mom.gameObject.SetActive(true), 
                () => _shouldReturn
            );
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);
        _shouldReturn = true;
    }
}
