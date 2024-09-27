public interface IStateEnemy
{
    public void EnterState(AEnemy enemy);
    public void ExitState(AEnemy enemy);
    public void Update(AEnemy enemy);
    public void FixedUpdate(AEnemy enemy);
}
