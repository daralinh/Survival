using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonForMagicBook : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text tmp;

    private Color originalColor;
    private float fadedAlpha;
    private float fullAlpha;

    private void Awake()
    {
        originalColor = tmp.color;
        fadedAlpha = 0.75f;
        fullAlpha = 1f;
        SetAlpha(fadedAlpha);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.SetCursor2();
        SetAlpha(fullAlpha);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.SetCursor1();
        SetAlpha(fadedAlpha);
    }
    private void SetAlpha(float alpha)
    {
        Color color = tmp.color;
        color.a = alpha;
        tmp.color = color;
    }
}
