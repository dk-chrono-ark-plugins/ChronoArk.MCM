using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
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
    protected readonly McmButton _apply;
    protected readonly McmButton _reset;
    private readonly McmText _titleText;

    public ModInfo Owner { get; init; }
    public string Title { get; set; }

    public McmPage(ModInfo modInfo)
    {
        Owner = modInfo;
        Title = $"{Owner.Title}  v{Owner.Version}";
        _titleText = new() { Content = Title };

        _apply = new() {
            Content = new McmText() {
                Content = "Apply",
                FixedSize = 26f,
            },
            OnClick = McmWindow.Save,
            Size = new(100f, 50f)
        };
        _reset = new() {
            Content = new McmText() {
                Content = "Reset",
                FixedSize = 26f,
            },
            OnClick = McmWindow.Reset,
            Size = new(100f, 50f)
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

        var page = parent.AttachRectTransformObject($"McmPage:{Owner.Title}");
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

        var apply = _apply.Render<RectTransform>(page);
        apply.AlignToBottom(new(-55f, -80f));

        var reset = _reset.Render<RectTransform>(page);
        reset.AlignToBottom(new(55f, -80f));

        Ref = page.gameObject;
        return page;
    }

    protected virtual void RenderPageElements(Transform parent)
    {
        foreach (var element in _elements) {
            element.Render(parent);
        }
    }
}
