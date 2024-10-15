public class GoldChest : AChest
{
    protected override void BackToPool()
    {
        base.BackToPool();
        UpgradeManager.Instance.RedBookUpgrade();
    }
}
