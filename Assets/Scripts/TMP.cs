using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TMP : MonoBehaviour
{
    public float moveSpeed;
    public float fadeDuration;
    private TextMeshProUGUI tmp;
    private RectTransform rectTransform;

    private Color originalColor;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        originalColor = tmp.color;
        gameObject.SetActive(false);
    }

    public void Born(string _context, Vector2 _position, Color _color)
    {
        rectTransform.position = _position;
        tmp.color = _color;
        tmp.text = _context;
        gameObject.SetActive(true);
        StartCoroutine(MoveAndFadeHandler());
    }

    private IEnumerator MoveAndFadeHandler()
    {
        originalColor = tmp.color;
        Vector3 originalPosition = rectTransform.position;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            rectTransform.position = originalPosition + Vector3.up * (moveSpeed * elapsedTime);

            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            tmp.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        BackToPool();
    }

    private void BackToPool()
    {
        PoolingTMP.Instance.BackToPool(this);
        gameObject.SetActive(false);
    }
}
