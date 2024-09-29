public class AttackStateEnemy : IStateEnemy
{
    public void EnterState(AEnemy enemy)
    {
        enemy.EnterAttackState();
    }

    public void ExitState(AEnemy enemy)
    {
        enemy.ExitAttackState();
    }

    public void FixedUpdate(AEnemy enemy)
    {
        enemy.FixedUpdateAttackState();
    }

    public void Update(AEnemy enemy)
    {
        enemy.UpdateAttackState();
    }
}
