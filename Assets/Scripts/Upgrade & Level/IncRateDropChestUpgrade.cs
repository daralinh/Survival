public class IncRateDropChestUpgrade : AUpgrade
{
    public override void Active()
    {
        PoolingItem.Instance.RateDropChest += 5;
    }

    public override string GetContent()
    {
        return "Tăng rate drop chest";
    }
}
