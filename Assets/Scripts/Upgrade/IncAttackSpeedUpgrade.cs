public class IncAttackSpeedUpgrade : AUpgrade
{
    public override void Active()
    {
        UpgradeManager.Instance.BuffAttackSpeed += 0.1f;
    }

    public override string GetContent()
    {
        return "Tăng 10% tốc độ đánh";
    }
}
