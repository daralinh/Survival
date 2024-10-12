public class IncAttackSpeedUpgrade : AUpgrade
{
    public override void Active()
    {
        UpgradeManager.Instance.BuffAttackSpeed += 0.1f;
    }

    public override void ShowText()
    {
        text.text = "Tăng 10% tốc độ đánh";
    }
}
