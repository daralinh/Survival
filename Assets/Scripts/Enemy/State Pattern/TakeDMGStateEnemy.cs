public class TakeDMGStateEnemy : IStateEnemy
{
    public void EnterState(AEnemy enemy)
    {
        enemy.EnterTakeDMGState();
    }

    public void ExitState(AEnemy enemy)
    {
        enemy.ExitTakeDMGState();
    }

    public void FixedUpdate(AEnemy enemy)
    {
        enemy.FixedUpdateTakeDMGState();
    }

    public void Update(AEnemy enemy)
    {
        enemy.UpdateTakeDMGState();
    }
}
