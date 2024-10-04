using System.Collections;
using UnityEngine;

public class PumpkinDude : AEnemy
{
    [SerializeField] private float timeToStopWhenTakeDMG;
    private Coroutine coroutineTakeDMG;

    protected override void Awake()
    {
        tag = ETag.PumpkinDude.ToString();
        base.Awake();
    }

    // Attack
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
        hpManager.FlashSprite();

        if (coroutineTakeDMG != null)
        {
            StopCoroutine(coroutineTakeDMG);
            coroutineTakeDMG = null;
        }

        coroutineTakeDMG = StartCoroutine(Handler());
    }
    private IEnumerator Handler()
    {
        yield return new WaitForSeconds(timeToStopWhenTakeDMG);
        currentState.ExitState(this);
    }

    // Death
    public override void EnterDeathState()
    {
        animator.SetTrigger(EAnimation.Idle.ToString());
        PoolingBullet.Instance.ShootRedExplosion(transform, ELayer.Player);
        currentState.ExitState(this);
    }

    public override void ExitDeathState()
    {
        gameObject.SetActive(false);
    }
}
