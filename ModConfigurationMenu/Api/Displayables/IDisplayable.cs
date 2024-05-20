using ChronoArkMod.Helper;

namespace MCM.Api.Displayables;

public interface IDisplayable
{
    /// <summary>
    /// The call to render!
    /// </summary>
    /// <param name="parent">Parent transform</param>
    /// <returns>If this is the last element, return itself;<br/>Otherwise return next available parent</returns>
    Transform Render(Transform parent);

    /// <summary>
    /// Unrender? Surrender!
    /// </summary>
    void Hide();
}


public static class DisplayableComponent
{
    public static T Render<T>(this IDisplayable displayable, Transform parent) where T : Component
    {
        return displayable.Render(parent).gameObject.GetOrAddComponent<T>();
    }
}