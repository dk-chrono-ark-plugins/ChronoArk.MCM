using ChronoArkMod.Helper;

namespace Mcm.Implementation;

#nullable enable

internal class ScriptRef : IDeferredUpdate, IDisplayable
{
    protected bool _dirty = true;
    protected bool _deferred = false;
    private object? _deferredLock = null;

    public GameObject? Ref { get; private set; }
    public virtual Vector2? Size { get; set; }
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
        _deferredLock = Ref;
        return parent;
    }

    public virtual void Show()
    {
        Ref?.SetActive(true);
    }

    public void Destroy()
    {
        if (Ref != null) {
            UnityEngine.Object.Destroy(Ref);
            Ref = null;
        }
    }

    public void DeferredUpdate()
    {
        if (_deferred) {
            return;
        }
        _deferred = true;
        CoroutineHelper.Deferred(
            () => {
                Update();
                _dirty = true;
                _deferred = false;
            },
            () => _deferredLock != null
        );
    }

    public virtual void Update()
    {
    }
}
