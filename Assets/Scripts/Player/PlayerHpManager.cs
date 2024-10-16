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

    public override void BuffOriginHp(float _valueToBuff)
    {
        originHp = Mathf.Max(_valueToBuff + originHp, originHp);
        healthBar.SetMaxValue((int)originHp);
        Heal(_valueToBuff);
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
        currentHp = Mathf.Max(0, currentHp - dmg);

        healthBar.SetCurrentValue((int)currentHp);

        if (currentHp == 0)
        {
            GameManager.Instance.LoseGame();
            return;
        }

        PoolingTMP.Instance.SpawnTMP($"-{dmg}", transform.position, EColor.White);

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
