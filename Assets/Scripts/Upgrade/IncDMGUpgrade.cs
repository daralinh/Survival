﻿public class IncDMGUpgrade : AUpgrade
{
    public override void Active()
    {
        UpgradeManager.Instance.BuffDMG += 7;
    }

    public override void ShowText()
    {
        text.text = "Tăng 7 sát thương từ đòn đánh";
    }
}
