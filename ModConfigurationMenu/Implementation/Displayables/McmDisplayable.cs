using Mcm.Implementation.Components;

namespace Mcm.Implementation.Displayables;

public class McmDisplayable : ScriptRef, IDisplayable
{
    public virtual void Hide()
    {
        Ref?.SetActive(false);
    }

    public virtual Transform Render(Transform parent)
    {
        Ref = parent.gameObject;
        _deferredLock = Ref;

        if (McmMod.Instance?._config?.AttachDebugComponent ?? false) {
            Ref.GetOrAddComponent<ScriptRefHolder>().Holder = this;
        }

        return parent;
    }

    public virtual void Show()
    {
        Ref?.SetActive(true);
    }
}