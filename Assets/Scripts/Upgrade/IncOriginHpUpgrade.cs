public class IncOriginHpUpgrade : AUpgrade
{
    public override void Active()
    {
        PlayerController.Instance.HpManager.BuffOriginHp(30);
    }

    public override string GetContent()
    {
        return "Tăng vĩnh viễn 30 máu";
    }
}
