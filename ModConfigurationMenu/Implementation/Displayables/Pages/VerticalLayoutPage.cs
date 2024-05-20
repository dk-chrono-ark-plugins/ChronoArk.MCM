using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using UnityEngine.UI;

namespace MCM.Implementation.Displayables;


/// <summary>
/// Grid layout page, will pass the same content holder to the render pipe
/// </summary>
internal sealed class VerticalLayoutPage(ModInfo Info) : ScrollViewPage(Info)
{
    public override Transform Render(Transform parent)
    {
        var content = base.Render(parent);

        var gridLayoutGroup = content.AddComponent<VerticalLayoutGroup>();
        gridLayoutGroup.spacing = 20f;
        gridLayoutGroup.padding = new(20, 20, 20, 20);
        gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;

        var contentSizeFitter = content.AddComponent<ContentSizeFitter>();
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        RenderPageElements(content);

        return content;
    }
}
