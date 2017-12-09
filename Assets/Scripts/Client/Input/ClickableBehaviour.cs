using UnityEngine;
using UnityEngine.EventSystems;

class ClickableBehaviour : MonoBehaviour, IPointerClickHandler
{
    public PointerClickEvent clickCallback;
    public object userData;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.dragging == true)
            return;
        if (clickCallback != null)
            clickCallback(eventData, userData);
    }

    public delegate void PointerClickEvent(PointerEventData eventData, object userData);
}
