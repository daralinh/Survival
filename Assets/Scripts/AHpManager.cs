using UnityEngine;

[RequireComponent(typeof(FlashSprite))]
[RequireComponent(typeof(KnockBack))]

public abstract class AHpManager : MonoBehaviour
{
    [SerializeField] protected float originHp;
    protected AEffectManager effectManager;
    protected float currentHp;

    protected FlashSprite flashSprite;
    protected KnockBack knockBack;

    protected virtual void Awake()
    {
        currentHp = originHp;
        flashSprite = GetComponent<FlashSprite>();
        knockBack = GetComponent<KnockBack>();
    }

    public virtual void TakeDMG(float dmg, Vector2 sourceDMG)
    {
        currentHp -= dmg;
    }

    public abstract void TakeEffect(AEffect aEffect);

    public void FlashSprite()
    {
        flashSprite.Flash();
    }

    public void KnockBack(Vector2 sourceDMG, float thurst)
    {
        knockBack.GetKnockBack(sourceDMG, thurst);
    }
}
