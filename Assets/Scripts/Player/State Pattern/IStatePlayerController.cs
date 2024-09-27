public interface IStatePlayerController
{
    public void EnterState(PlayerController playerController);
    public void ExitState(PlayerController playerController);
    public void Update(PlayerController playerController);
    public void FixedUpdate(PlayerController playerController);
}
