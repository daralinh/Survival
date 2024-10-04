using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : ASpell
{
    protected override void  Handler()
    {
        Collider2D[] objectsHit =
                Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask(ELayer.Enemy.ToString()));

        countNumberShootInPerAttack = 0;

        foreach (Collider2D _object in objectsHit)
        {
            PoolingBullet.Instance.ShootLightning(_object.transform.position, ELayer.Enemy);

            if (++countNumberShootInPerAttack == numberShootInPerAttack)
            {
                break;
            }
        }
    }
}
