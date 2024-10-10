using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Cannon : AWeapon
{
    [SerializeField] private float recoilStrength;
    [SerializeField] private float maxRecoilAngle;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override IEnumerator Shooting()
    {
        isShooting = true;
        animator.SetBool(EAnimation.Shoot.ToString(), true);
        PoolingBullet.Instance.ShootBulletCCannon(transform, mousePointPosition, targetLayer);
        yield return new WaitForSeconds(timeShoot);
        animator.SetBool(EAnimation.Shoot.ToString(), false);
        isShooting = false;
    }

    protected override Vector2 GetDirectionFollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        aimDirection = (mousePosition - transform.position).normalized;

        float randomAngle = Random.Range(-maxRecoilAngle, maxRecoilAngle);
        aimDirection = Quaternion.Euler(0, 0, randomAngle) * aimDirection;

        return (Vector2)aimDirection;
    }
}
