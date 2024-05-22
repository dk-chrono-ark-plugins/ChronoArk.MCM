using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using I2.Loc;
using Mcm.Common;
using Mcm.Implementation.Components;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

#nullable enable

/// <summary>
/// Basic page layout, will return a bordered page to the render pipe
/// </summary>
internal class McmPage : ScriptRef, IPage
{
    protected readonly List<IDisplayable> _elements = [];
    protected readonly McmComposite _buttons;
    protected readonly McmText _titleText;

    public ModInfo Owner { get; init; }
    public string Title { get; set; }
    public List<IDisplayable> Elements => _elements;
    public string Name { get; set; }

    public McmPage(ModInfo modInfo)
    {
        Name = "";
        Owner = modInfo;
        Title = $"{Owner.Title}  v{Owner.Version}";
        _titleText = new() { Content = Title };

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

    public virtual void Add(IDisplayable displayable)
    {
        _elements.Add(displayable);
    }

    public virtual void Clear()
    {
        _elements.Clear();
    }

    public virtual void Remove(IDisplayable displayable)
    {
        _elements.Remove(displayable);
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        McmManager.ResetModSetting(Owner);

        var page = parent.AttachRectTransformObject($"McmPage:{Owner.Title}:{Name}");
        page.sizeDelta = PageSizeFitter.Normal + PageSizeFitter.BorderThickness;

        var imageBg = page.AddComponent<Image>();
        imageBg.color = Color.black;

        var imageFg = page.AddComponent<Outline>();
        imageFg.effectColor = Color.white;
        imageFg.effectDistance = PageSizeFitter.BorderThickness;

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
