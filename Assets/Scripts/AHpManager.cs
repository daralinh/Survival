using UnityEngine;

[RequireComponent(typeof(FlashSprite))]

public abstract class AHpManager : MonoBehaviour
{
    [SerializeField] protected float originHp;
    protected float currentHp;
    protected FlashSprite flashSprite;

    protected virtual void Awake()
    {
        currentHp = originHp;
        flashSprite = GetComponent<FlashSprite>();
    }

    public abstract void TakeDMG(float dmg, Vector2 sourceDMG, EEffectApplied effectApplied);

    public void FlashSprite()
    {
        flashSprite.Flash();
    }
}
