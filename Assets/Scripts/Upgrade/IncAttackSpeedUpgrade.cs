public class IncAttackSpeedUpgrade : AUpgrade
{
    public override void Active()
    {
        UpgradeManager.Instance.BuffAttackSpeed += 0.75f;
    }

    public override string GetContent()
    {
        return "Tăng 75% tốc độ đánh";
    }
}
