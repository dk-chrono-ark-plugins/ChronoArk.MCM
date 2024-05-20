namespace MCM.Api.Displayables;

#nullable enable

public interface IButton : IDisplayable
{
    IDisplayable Content { get; }
    bool Interactable { get; }
    Action OnClick { get; }
    Vector2? Size { get; }

    void Click();
}
