public class AddFireStormSpellUpgrade : AUpgrade
{
    public override void Active()
    {
        SpellManager.Instance.AddFireStormSpell();
    }

    public override string GetContent()
    {
        return "Trang bị Fire Storm Spell. Sau vài gây sẽ tạo ra 1 con lốc xoáy lửa";
    }
}
