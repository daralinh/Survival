using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image UIImage;
    private Image image;
    private float fadedAlpha;
    private float fullAlpha;

    private Color originalColor;
    private float originTime;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
        fadedAlpha = 0.5f;
        fullAlpha = 1f;
        SetAlpha(fadedAlpha);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Open();
        }
    }

    public void Open()
    {
        GameManager.Instance.Pause();
        UIImage.gameObject.SetActive(true);
    }

    public void Close()
    {
        UIImage.gameObject.SetActive(false);
        GameManager.Instance.Continuous();
        CursorManager.Instance.SetCursor1();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetAlpha(fullAlpha);
        CursorManager.Instance.SetCursor2();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetAlpha(fadedAlpha);
        CursorManager.Instance.SetCursor1();
    }

    private void SetAlpha(float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
