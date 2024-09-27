public class MoveStateEnemy : IStateEnemy
{
    public void EnterState(AEnemy enemy)
    {
        enemy.EnterMoveState();
    }

    public void ExitState(AEnemy enemy)
    {
        enemy.ExitMoveState();
    }

    public void FixedUpdate(AEnemy enemy)
    {
        enemy.FixedUpdateMoveState();
    }

    public void Update(AEnemy enemy)
    {
        enemy.UpdateMoveState();
    }
}
