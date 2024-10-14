public class AddFireStepSpellUpgrade : AUpgrade
{
    public override void Active()
    {
        SpellManager.Instance.AddFireStepSpell();
    }

    public override string GetContent()
    {
        return "Trang bị FireStep Spell. Sau vài gây tạo ra 1 vòng tròn lửa tại vị trí của bạn";
    }
}
