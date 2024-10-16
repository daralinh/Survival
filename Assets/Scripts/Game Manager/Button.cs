using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.SetCursor2();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.SetCursor1();
    }
}
