public class IncOriginHpUpgrade : AUpgrade
{
    public override void Active()
    {
        PlayerController.Instance.HpManager.BuffOriginHp(30);
    }

    public override void ShowText()
    {
        text.text = "Tăng vĩnh viễn 30 máu";
    }
}
