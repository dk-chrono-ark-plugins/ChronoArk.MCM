namespace ModConfigurationMenu.Api.Displayables;

public interface IDisplayable
{
    /// <summary>
    /// The call to render!
    /// </summary>
    /// <param name="parent">Parent transform</param>
    void Render(Transform parent);
}
