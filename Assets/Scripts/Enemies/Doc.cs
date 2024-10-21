using System.Collections;
using UnityEngine;

public class Doc : AEnemy
{
    private Coroutine coroutineTakeDMG;

    protected override void Awake()
    {
        tag = ETag.Doc.ToString();
        base.Awake();
    }

    public override void Born(Vector2 _position)
    {
        coroutineTakeDMG = null;
        base.Born(_position);
    }

    // Attack State
    public override void EnterAttackState()
    {
        currentState.ExitState(this);
    }

    public override void ExitAttackState()
    {
        ChangeStateToDeathState();        
    }

    // TakeDMG State
    public override void EnterTakeDMGState()
    {
       // hpManager.FlashSprite();

        if (coroutineTakeDMG != null)
        {
            StopCoroutine(coroutineTakeDMG);
            coroutineTakeDMG = null;
        }

        coroutineTakeDMG = StartCoroutine(Handler());
    }
    private IEnumerator Handler()
    {
        yield return new WaitForSeconds(0.15f);
        currentState.ExitState(this);
    }

    // Death State
    public override void EnterDeathState()
    {
        animator.SetTrigger(EAnimation.Idle.ToString());
        PoolingBullet.Instance.ShootGreenExplosion(transform, ELayer.Player);
        currentState.ExitState(this);
    }

    public override void ExitDeathState()
    {
        gameObject.SetActive(false);
        PoolingEnemy.Instance.BackToPool(this);
    }
}
