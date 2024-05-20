using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using MCM.Api.Displayables;
using MCM.Common;
using UnityEngine.UI;

namespace MCM.Implementation.Displayables;

#nullable enable

/// <summary>
/// Basic page layout, will pass a bordered panel to the render pipe
/// </summary>
internal class McmPage : ScriptRef, IPage
{
    protected readonly List<IDisplayable> _elements = [];
    private readonly McmText _titleText;

    public ModInfo Owner { get; init; }
    public string? Title { get; set; }

    public McmPage(ModInfo modInfo)
    {
        Owner = modInfo;
        Title = $"{Owner.Title}  v{Owner.Version}";
        _titleText = new() { Content = Title };
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
        var page = parent.AttachRectTransformObject($"McmPage:{Owner.Title}");
        page.sizeDelta = PageSizeFitter.Normal + PageSizeFitter.BorderThickness;

        var imageBg = page.AddComponent<Image>();
        imageBg.color = Color.black;
        var imageFg = page.AddComponent<Outline>();
        imageFg.effectColor = Color.white;
        imageFg.effectDistance = PageSizeFitter.BorderThickness;

        var title = _titleText.Render<RectTransform>(page);
        title.AlignToTop(-20f);
        title.pivot = new(0.5f, 0f);
        title.sizeDelta = page.sizeDelta with { y = 50f };

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
