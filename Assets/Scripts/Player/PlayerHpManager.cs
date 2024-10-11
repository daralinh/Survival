using UnityEngine;

public class PlayerHpManager : AHpManager
{
    public HealthBar healthBar;

    protected override void Awake()
    {
        base.Awake();
        effectManager = GetComponent<PlayerEffectManager>();
        healthBar.SetMaxValue((int)originHp);
        healthBar.SetCurrentValue((int)currentHp);
    }

    public override void HealFullHp()
    {
        base.HealFullHp();
        healthBar.SetCurrentValue((int)currentHp);
    }

    public override void Heal(float _valueToHeal)
    {
        base.Heal(_valueToHeal);
        healthBar.SetCurrentValue((int)currentHp);
    }

    public override void TakeDMG(float dmg, Vector2 sourceDMG)
    {
        base.TakeDMG(dmg, sourceDMG);

        healthBar.SetCurrentValue((int)currentHp);
        knockBack.GetKnockBack(sourceDMG, 0.5f);
        flashSprite.Flash();


        if (currentHp > 0)
        {
            PlayerController.Instance.TakeDMGHandler(sourceDMG);
        }
        else
        {
        }
    }

    public override void TakeEffect(AEffect aEffect)
    {
        effectManager.ActiveEffect(aEffect);
    }
}
