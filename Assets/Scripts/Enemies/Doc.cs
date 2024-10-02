using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doc : AEnemy
{
    protected override void Awake()
    {
        tag = ETag.Doc.ToString();
        base.Awake();
    }
    public override void EnterAttackState()
    {
        animator.SetTrigger(EAnimation.Run.ToString());
        currentState.ExitState(this);
    }

    public override void ExitAttackState()
    {
        ChangeStateToDeathState();
    }

    public override void EnterDeathState()
    {
        animator.SetTrigger(EAnimation.Idle.ToString());
        PoolingBullet.Instance.ShootGreenExplosion(transform, ELayer.Player);
        currentState.ExitState(this);
    }

    public override void ExitDeathState()
    {
        gameObject.SetActive(false);
    }
}
