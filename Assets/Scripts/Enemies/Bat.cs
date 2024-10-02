using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : AEnemy
{
    public override void ExitAttackState()
    {
        PlayerController.Instance.HpManager.TakeDMG(dmg, transform.position);
        base.ExitAttackState();
    }

    public override void EnterTakeDMGState()
    {
        base.EnterTakeDMGState();
        hpManager.FlashSprite();
    }
}
