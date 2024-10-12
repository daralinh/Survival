public class AddFireBallSpellUpgrade : AUpgrade
{
    public override void Active()
    {
        SpellManager.Instance.AddFireBallSpell();
    }

    public override void ShowText()
    {
        text.text = "Trang bị thêm FireBall Spell, sau vài giây sẽ tự động bắn ra những quả cầu lửa";
    }
}
