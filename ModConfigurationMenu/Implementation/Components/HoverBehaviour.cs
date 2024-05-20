using UnityEngine.EventSystems;

namespace Mcm.Implementation.Components;

internal class HoverBehaviour : UIBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
