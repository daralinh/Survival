public class AddLightningSpellUpgrade : AUpgrade
{
    public override void Active()
    {
        SpellManager.Instance.AddLightningSpell();
    }

    public override string GetContent()
    {
        return "Trang bị thêm Lightning Spell, sau vài giây sẽ bắn ra 1 tia sét về phía kẻ địch";
    }
}
