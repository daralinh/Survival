public class PumpkinDude : AEnemy
{
    protected override void Awake()
    {
        tag = ETag.PumpkinDude.ToString();
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
        PoolingBullet.Instance.ShootRedExplosion(transform, ELayer.Player);
        currentState.ExitState(this);
    }

    public override void ExitDeathState()
    {
        gameObject.SetActive(false);
    }
}
