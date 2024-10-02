public class PumpkinDude : AEnemy
{
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
