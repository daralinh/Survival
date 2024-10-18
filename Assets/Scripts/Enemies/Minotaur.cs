public class Minotaur : AEnemy
{
    protected override void Awake()
    {
        tag = ETag.Minotaur.ToString();
        base.Awake();
    }

    // Attack

    public override void ExitAttackState()
    {
        PlayerController.Instance.HpManager.TakeDMG(dmg, transform.position);
        base.EnterAttackState();
    }
}
