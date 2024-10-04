using UnityEngine;

[RequireComponent(typeof(Animator))]

public class FireBall : ABullet
{
    private Animator animator;
    protected override void Born()
    {
        tag = ETag.FireBall.ToString();
        animator = GetComponent<Animator>();
        spriteRenderer.sortingOrder = 3;
        base.Born();
    }

    public override void StartShooting(Transform source, Vector2 targetPosition, ELayer _targetLayer)
    {
        transform.position = source.position;
        transform.rotation = Quaternion.identity;
        FlipAndRotateFollowTarget(targetPosition);
        moveDir = (targetPosition - rb2D.position).normalized;
        targetLayer = _targetLayer;
        isShooting = true;
        gameObject.SetActive(true);
        animator.SetTrigger(EAnimation.Shoot.ToString());
    }

    protected override void BackToPool()
    {
        animator.SetTrigger(EAnimation.Idle.ToString());
        base.BackToPool();
    }
}
