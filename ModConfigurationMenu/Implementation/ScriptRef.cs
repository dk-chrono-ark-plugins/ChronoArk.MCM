namespace Mcm.Implementation;

#nullable enable

internal class ScriptRef : IDeferredUpdater, IDisplayable
{
    protected bool _dirty = true;
    protected bool _deferred = false;

    public GameObject? Ref { get; private set; }
    public virtual Vector2? Size { get; init; }
    public bool Deferred { get; }
    public bool Dirty => _dirty;

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
        Ref = parent.gameObject;
        return parent;
    }

    public virtual void Show()
    {
        Ref?.SetActive(true);
    }

    public void Destroy()
    {
        if (Ref != null) {
            UnityEngine.Object.DestroyImmediate(Ref);
            Ref = null;
        }
    }

    public virtual void DeferredUpdate()
    {
        throw new NotImplementedException();
    }
}
