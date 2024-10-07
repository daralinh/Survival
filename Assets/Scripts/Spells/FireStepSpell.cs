using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStepSpell : ASpell
{
    protected override void Handler()
    {
        PoolingBullet.Instance.ShootFireSpot(PlayerController.Instance.transform, Vector2.zero, ELayer.Enemy);
        base.Handler();
    }
}
