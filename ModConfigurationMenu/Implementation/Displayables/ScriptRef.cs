using MCM.Api.Displayables;

namespace MCM.Implementation.Displayables;

internal class ScriptRef : IDisplayable, IScriptRef
{
    public GameObject Ref { get; protected set; }

    public virtual void Hide()
    {
        Ref?.SetActive(false);
    }

    public virtual Transform Render(Transform parent)
    {
        throw new NotImplementedException();
    }
}
