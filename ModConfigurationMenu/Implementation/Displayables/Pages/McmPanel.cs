using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using I2.Loc;
using Mcm.Implementation.Components;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

/// <summary>
/// Basic page layout, will return a bordered page to the render pipe
/// </summary>
internal class McmPanel : McmPage
{
    protected readonly McmComposite _buttons;
    protected readonly McmText _titleText;

    public override string Title
    {
        get => _titleText.Content;
        set => _titleText.Content = value;
    }

    public McmPanel(ModInfo modInfo) : base(modInfo)
    {
        _titleText = new() { Content = string.Empty };
        Title = $"{Owner.Title}  v{Owner.Version}";

        var back = new McmButton() {
            Content = new McmText() {
                Content = "<b><<</b>",
                FontSize = 40f,
            },
            OnClick = McmWindow.Back
        };
        var apply = new McmButton() {
            Content = new McmText() {
                Content = LocalizationManager.GetTranslation(ScriptTerms.UI.Apply),
                FontSize = 26f,
            },
            OnClick = McmWindow.Save
        };
        var reset = new McmButton() {
            Content = new McmText() {
                Content = LocalizationManager.GetTranslation(ScriptTerms.UI.Cancel),
                FontSize = 26f,
            },
            OnClick = McmWindow.Reset
        };
        _buttons = new(ICompositeLayout.LayoutGroup.Horizontal) {
            Composites = [
                new(back, new(100f, 50f)),
                new(apply, new(100f, 50f)),
                new(reset, new(100f, 50f)),
            ],
            Spacing = new(10f, 0f),
            Size = new(400f, 50f),
        };
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var page = parent.AttachRectTransformObject($"McmPage:{Owner.Title}:{Name}");
        page.sizeDelta = PageStyle.Normal + PageStyle.BorderThickness;

        var imageBg = page.AddComponent<Image>();
        imageBg.color = PageStyle.BackColor;

        var imageFg = page.AddComponent<Outline>();
        imageFg.effectColor = PageStyle.BorderColor;
        imageFg.effectDistance = PageStyle.BorderThickness;

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
