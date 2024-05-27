using ChronoArkMod.ModData;
using Mcm.Implementation.Components;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

/// <summary>
///     Basic page layout, will return a bordered page to the render pipe
/// </summary>
internal class McmPanel : McmPage
{
    private readonly McmComposite _buttons;
    private readonly McmText _titleText;

    protected McmPanel(ModInfo modInfo)
        : base(modInfo)
    {
        _titleText = new() {
            Content = string.Empty,
            Style = {
                TextFontSize = 40f,
            },
        };

        Title = $"{Owner.Title}  v{Owner.Version}";

        var back = new McmButton {
            Content = new McmText(Style) {
                Content = McmLoc.Page.Back,
            },
            OnClick = McmWindow.PageBack,
        };
        var apply = new McmButton {
            Content = new McmText(Style) {
                Content = McmLoc.Page.Apply,
            },
            OnClick = McmWindow.PageSave,
        };
        var reset = new McmButton {
            Content = new McmText(Style) {
                Content = McmLoc.Page.Reset,
            },
            OnClick = McmWindow.PageReset,
        };

        _buttons = new(ICompositeLayout.LayoutGroup.Horizontal) {
            Composites = [
                new(back, new(100f, 50f)),
                new(apply, new(100f, 50f)),
                new(reset, new(100f, 50f)),
            ],
            Style = {
                LayoutSpacing = new(10f, 0f),
                Size = new(400f, 50f),
            },
        };
    }

    public sealed override string Title
    {
        get => _titleText.Content;
        set => _titleText.Content = value;
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var page = parent.AttachRectTransformObject($"McmPage:{Owner.Title}:{Name}");
        page.sizeDelta = Style.Size!.Value + Style.OutlineSize!.Value;

        var imageBg = page.AddComponent<Image>();
        imageBg.color = Style.ColorPrimary!.Value;

        var imageFg = page.AddComponent<Outline>();
        imageFg.effectColor = Style.ColorSecondary!.Value;
        imageFg.effectDistance = Style.OutlineSize!.Value;

        var title = _titleText.Render<RectTransform>(page);
        title.AlignToTop(new(0f, 20f));
        title.pivot = new(0.5f, 0f);
        title.sizeDelta = page.sizeDelta with { y = 50f };

        var buttons = _buttons.Render<RectTransform>(page);
        buttons.AlignToBottom(new(0f, -80f));

        return base.Render(page);
    }

    protected virtual void RenderPageElements(Transform parent)
    {
        foreach (var element in _elements) {
            element.Render(parent);
        }
    }
}