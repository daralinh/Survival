using System.Collections;
using UnityEngine;

public class FireBallSpell : ASpell
{
    protected override void Handler()
    {
        Collider2D[] objectsHit =
                Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask(ELayer.Enemy.ToString()));

        countNumberShootInPerAttack = 0;

        foreach (Collider2D _object in objectsHit)
        {
            PoolingBullet.Instance.ShootFireBall(transform, _object.transform.position, ELayer.Enemy);

            if (++countNumberShootInPerAttack == numberShootInPerAttack)
            {
                break;
            }
        }
    }
}
