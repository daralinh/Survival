public class Bat : AEnemy
{
    protected override void Awake()
    {
        tag = ETag.Bat.ToString();
        base.Awake();
    }

    public override void ExitAttackState()
    {
        PlayerController.Instance.HpManager.TakeDMG(dmg, transform.position);
        base.ExitAttackState();
    }

    public override void EnterTakeDMGState()
    {
        base.EnterTakeDMGState();
        hpManager.FlashSprite();
    }
}
