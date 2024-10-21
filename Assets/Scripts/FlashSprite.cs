using System.Collections;
using UnityEngine;

public class FlashSprite : MonoBehaviour
{
    public FlashSpriteData flashSpriteData;

    public float flashTime => flashSpriteData.Stats.flashTime;
    public Material whiteMaterial => flashSpriteData.Stats.whiteMaterial;
    
    private SpriteRenderer spriteRenderer;
    private Material originMaterial;
    private bool isFlashing;
    private Coroutine coroutine;

    private void Awake()
    {
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
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.material = originMaterial;
        isFlashing = false;
    }
}