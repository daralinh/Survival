public class DecCoolDownSpellUpgrade : AUpgrade
{
    public override void Active()
    {
        UpgradeManager.Instance.DecCoolDownSpell += 1f;
    }

    public override string GetContent()
    {
        return "Giảm thời gian hồi tất cả spell đi 1 giây";
    }
}
