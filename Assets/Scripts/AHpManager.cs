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
        originHp = Mathf.Max(1, originHp);
        currentHp = originHp;
        flashSprite = GetComponent<FlashSprite>();
        knockBack = GetComponent<KnockBack>();
    }

    public virtual void TakeDMG(float dmg, Vector2 sourceDMG)
    {
        if (currentHp == 0)
        {
            return;
        }

        currentHp = Mathf.Max(0, currentHp - dmg);
    }

    public bool IsFullHp()
    {
        return (currentHp == originHp) ? true : false;
    }

    public void BuffOriginHp(float _valueToBuff)
    {
        originHp = Mathf.Max(_valueToBuff + originHp, originHp);
        Heal(_valueToBuff);
    }

    public void HealFullHp()
    {
        currentHp = originHp;
        flashSprite.BackToOriginMaterial();
    }

    public void Heal(float _valueToHeal)
    {
        currentHp = Mathf.Min(currentHp + _valueToHeal, originHp);
        flashSprite.BackToOriginMaterial();
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
