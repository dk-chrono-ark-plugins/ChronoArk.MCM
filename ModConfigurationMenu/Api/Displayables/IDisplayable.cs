namespace Mcm.Api.Displayables;

/// <summary>
///     Basic renderable component
/// </summary>
public interface IDisplayable : IScriptRef
{
    /// <summary>
    ///     The call to render! This is called to instantiate the displayable
    /// </summary>
    /// <param name="parent">Parent transform</param>
    /// <returns>If this displayable is not meant to be chained, return itself;<br />Otherwise return next available parent</returns>
    Transform Render(Transform parent);

    /// <summary>
    ///     Set inactive
    /// </summary>
    void Hide();

    /// <summary>
    ///     Set active
    /// </summary>
    void Show();
}

public static class DisplayableComponent
{
    /// <summary>
    ///     Render helper func for easier component fetching
    /// </summary>
    /// <typeparam name="T">
    ///     <see cref="Component" />
    /// </typeparam>
    /// <param name="displayable"></param>
    /// <param name="parent"></param>
    /// <returns>Get as <see cref="Component" /></returns>
    public static T Render<T>(this IDisplayable displayable, Transform parent) where T : Component
    {
        return displayable.Render(parent).gameObject.GetOrAddComponent<T>();
    }
}