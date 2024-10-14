public class AddKunaiSpellUpgrade : AUpgrade
{
    public override void Active()
    {
        SpellManager.Instance.AddKunaiSpell();
    }

    public override string GetContent()
    {
        return "Trang bị Kunai Spell. Sau vài giây bắn ra các phi tiêu về phía kẻ địch";
    }
}
