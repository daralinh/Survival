public class DeathStateEnemy : IStateEnemy
{
    public void EnterState(AEnemy enemy)
    {
        enemy.EnterDeathState();
    }

    public void ExitState(AEnemy enemy)
    {
        enemy.ExitDeathState();
    }

    public void FixedUpdate(AEnemy enemy)
    {
        enemy.FixedUpdateDeathState();
    }

    public void Update(AEnemy enemy)
    {
        enemy.UpdateDeathState();
    }
}
