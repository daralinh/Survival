using System.Collections;
using UnityEngine;

public class FlashSprite : MonoBehaviour
{
    [SerializeField] private float timeFlashSprite;
    [SerializeField] private Material whiteMaterial;

    private SpriteRenderer spriteRenderer;
    private Material originMaterial;
    private bool isFlashing;
    private Coroutine coroutine;

    private void Awake()
    {
        timeFlashSprite = Mathf.Max(timeFlashSprite, 0.1f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        originMaterial = spriteRenderer.material;
        isFlashing = false;
        coroutine = null;
    }

    public void BackToOriginMaterial()
    {
        spriteRenderer.material = originMaterial;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    public void Flash()
    {
        if (!isFlashing)
        {
            coroutine = StartCoroutine(Handler());
        }
    }

    private IEnumerator Handler()
    {
        isFlashing = true;
        spriteRenderer.material = whiteMaterial;
        yield return new WaitForSeconds(timeFlashSprite);
        spriteRenderer.material = originMaterial;
        isFlashing = false;
    }
}