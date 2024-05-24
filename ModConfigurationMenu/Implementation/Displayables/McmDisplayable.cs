namespace Mcm.Implementation.Displayables;

internal class McmDisplayable : ScriptRef, IDisplayable
{
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
}