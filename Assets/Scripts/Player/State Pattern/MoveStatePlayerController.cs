public class MoveStatePlayerController : IStatePlayerController
{
    public void EnterState(PlayerController playerController)
    {
        playerController.ChangeSpeedForMoveState();
        playerController.SetTriggerAnimation(EAnimation.Idle);
    }

    public void ExitState(PlayerController playerController)
    {
    }

    public void FixedUpdate(PlayerController playerController)
    {
        playerController.MoveFollowDirection();
    }

    public void Update(PlayerController playerController)
    {
        playerController.GetInputMove();
        playerController.FlipSpriteFollowMouse();
    }
}
