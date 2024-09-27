public class TakeDMGStatePlayerController : IStatePlayerController
{
    public void EnterState(PlayerController playerController)
    {
        playerController.EnterTakeDMGState();
    }

    public void ExitState(PlayerController playerController)
    {
        playerController.ChangeSpeedForMoveState();
    }

    public void FixedUpdate(PlayerController playerController)
    {
    }

    public void Update(PlayerController playerController)
    {
    }
}
