using System.Collections;
using UnityEngine;

public class FlashSprite : MonoBehaviour
{
    [SerializeField] private float timeFlashSprite;
    [SerializeField] private Material whiteMaterial;

    private SpriteRenderer spriteRenderer;
    private Material originMaterial;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        StartCoroutine(Handler());
    }

    private IEnumerator Handler()
    {
        spriteRenderer.material = whiteMaterial;
        yield return new WaitForSeconds(timeFlashSprite);
        spriteRenderer.material = originMaterial;
    }
}