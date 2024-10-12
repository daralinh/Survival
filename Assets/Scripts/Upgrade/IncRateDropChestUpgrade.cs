public class IncRateDropChestUpgrade : AUpgrade
{
    public override void Active()
    {
        PoolingItem.Instance.RateDropChest += 5;
    }

    public override void ShowText()
    {
        throw new System.NotImplementedException();
    }
}
