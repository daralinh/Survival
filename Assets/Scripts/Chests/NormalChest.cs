public class NormalChest : AChest
{
    protected override void BackToPool()
    {
        base.BackToPool();
        UpgradeManager.Instance.TakeExp(100);
    }
}
