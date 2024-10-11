using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleMedicine : AItem
{
    [SerializeField] private int hpToBuff;

    protected override void BackToBool()
    {
        PlayerController.Instance.HpManager.BuffOriginHp(hpToBuff);
        base.BackToBool();
    }
}
