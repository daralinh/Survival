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

    public void Open()
    {
        originTime = Time.timeScale;
        Time.timeScale = 0;
        UIImage.gameObject.SetActive(true);
    }

    public void Close()
    {
        UIImage.gameObject.SetActive(false);
        Time.timeScale = originTime;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetAlpha(fullAlpha);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetAlpha(fadedAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
