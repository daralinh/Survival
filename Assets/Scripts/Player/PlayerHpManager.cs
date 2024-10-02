using UnityEngine;

public class PlayerHpManager : AHpManager
{
    protected override void Awake()
    {
        base.Awake();
        effectManager = GetComponent<PlayerEffectManager>();
    }

    public override void TakeDMG(float dmg, Vector2 sourceDMG)
    {
        base.TakeDMG(dmg, sourceDMG);

        Debug.Log("take dmg");

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
