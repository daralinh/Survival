using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleMedicine : AItem
{
    [SerializeField] private int hpToBuff;

    protected override void BackToBool()
    {
        MusicManager.Instance.PlaySFX(EMusic.GetBottle);
        PlayerController.Instance.HpManager.BuffOriginHp(hpToBuff);
        base.BackToBool();
    }
}
