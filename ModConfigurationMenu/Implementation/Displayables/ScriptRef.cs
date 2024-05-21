namespace Mcm.Implementation.Displayables;

#nullable enable

internal class ScriptRef : IDisplayable
{
    public GameObject? Ref { get; protected set; }

    ~ScriptRef()
    {
        Destroy();
    }

    public virtual void Hide()
    {
        Ref?.SetActive(false);
    }

    public virtual Transform Render(Transform parent)
    {
        throw new NotImplementedException();
    }

    public virtual void Show()
    {
        Ref?.SetActive(true);
    }

    public void Destroy()
    {
        if (Ref != null) {
            UnityEngine.Object.DestroyImmediate(Ref);
        }
    }
}
