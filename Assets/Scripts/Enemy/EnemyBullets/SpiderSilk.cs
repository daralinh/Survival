using UnityEngine;

[RequireComponent(typeof(Animator))]

public class SpiderSilk : AEnemyBullet
{
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer.sortingOrder = 1;
        animator = GetComponent<Animator>();
    }

    public void StopMove()
    {
        speed = 0;
    }

    public override void StartShooting(Vector2 direction)
    {
        speed = originSpeed;
        base.StartShooting(direction);
        animator.SetBool(EAnimation.Shoot.ToString(), true);
    }

    protected override void BackToPool()
    {
        animator.SetBool(EAnimation.Shoot.ToString(), false);
        base.BackToPool();
    }
}
