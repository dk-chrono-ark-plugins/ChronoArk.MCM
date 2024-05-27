using Object = UnityEngine.Object;

namespace Mcm.Implementation;

internal class ScriptRef : IScriptRef
{
    protected object? _deferredLock = null;

    public GameObject? Ref { get; protected set; }
    public bool Deferred { get; private set; }
    public bool Dirty { get; private set; } = true;

    public void Destroy()
    {
        if (!Ref) {
            return;
        }

        Object.Destroy(Ref);
        Ref = null;
    }

    public void DeferredUpdate()
    {
        if (Deferred) {
            return;
        }

        Dirty = true;
        Deferred = true;
        CoroutineHelper.Deferred(
            () => {
                Update();
                Dirty = false;
                Deferred = false;
            },
            () => _deferredLock != null
        );
    }

    public virtual void Update()
    {
    }

    ~ScriptRef()
    {
        Destroy();
    }
}