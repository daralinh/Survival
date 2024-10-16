using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Cannon : AWeapon
{
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void PlaySFXWhenShoot()
    {
        MusicManager.Instance.PlayPlayerWeapon(EMusic.CannonC);
    }

    protected override IEnumerator Shooting()
    {
        isShooting = true;
        animator.SetBool(EAnimation.Shoot.ToString(), true);
        PoolingBullet.Instance.ShootBulletCCannon(transform, mousePointPosition, targetLayer);
        yield return new WaitForSeconds(AttackSpeed);
        animator.SetBool(EAnimation.Shoot.ToString(), false);
        isShooting = false;
    }

    protected override Vector2 GetDirectionFollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return (mousePosition - transform.position).normalized;
    }
}
