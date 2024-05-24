using ChronoArkMod.Helper;

namespace Mcm.Implementation;

#nullable enable

internal class ScriptRef : IScriptRef
{
    protected bool _dirty = true;
    protected bool _deferred = false;
    protected object? _deferredLock = null;

    public GameObject? Ref { get; protected set; }
    public bool Deferred { get; }
    public bool Dirty => _dirty;

    ~ScriptRef()
    {
        Destroy();
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
