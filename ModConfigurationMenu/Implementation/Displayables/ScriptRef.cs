namespace Mcm.Implementation.Displayables;

internal class ScriptRef : IDisplayable, IScriptRef
{
    public GameObject Ref { get; protected set; }

    ~ScriptRef()
    {
        if (Ref != null) {
            UnityEngine.Object.DestroyImmediate(Ref);
        }
    }

    public virtual void Hide()
    {
        Ref?.SetActive(false);
    }

    public virtual Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        throw new NotImplementedException();
    }
}
