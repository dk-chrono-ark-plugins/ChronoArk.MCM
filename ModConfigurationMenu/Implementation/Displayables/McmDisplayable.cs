using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcm.Implementation.Displayables;

#nullable enable
internal class McmDisplayable : ScriptRef, IDisplayable
{
    public virtual Vector2? Size { get; set; }

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
