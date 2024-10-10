using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBottle : AItem
{
    [SerializeField] private float valueHpToHeal;

    protected override void BackToBool()
    {
        PlayerController.Instance.HpManager.Heal(valueHpToHeal);
        base.BackToBool();
    }
}
