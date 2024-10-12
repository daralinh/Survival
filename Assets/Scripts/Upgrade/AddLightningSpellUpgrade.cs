public class AddLightningSpellUpgrade : AUpgrade
{
    public override void Active()
    {
        SpellManager.Instance.AddLightningSpell();
    }

    public override void ShowText()
    {
        text.text = "Trang bị thêm Light Spell, sau vài giây sẽ bắn ra 1 tia sét về phía kẻ địch";
    }
}
