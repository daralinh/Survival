using UnityEngine;

[RequireComponent(typeof(Animator))]

public class FireStorm : ABullet
{
    protected Animator animator;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        base.Awake();
    }

    public override void StartShooting(Transform _source, Vector2 _targetPosition, ELayer _targetLayer)
    {
        sourceTransform = _source;
        gameObject.transform.position = _source.position;
        FlipFollowTarget(_targetPosition);
        moveDir = (_targetPosition - rb2D.position).normalized;
        targetLayer = _targetLayer;
        isShooting = true;
        gameObject.SetActive(true);
        MusicManager.Instance.PlayBulletSFX(EMusic.FireStorm);
        animator.SetTrigger(EAnimation.Shoot.ToString());
    }

    protected override void BackToPool()
    {
        animator.SetTrigger(EAnimation.Idle.ToString());
        base.BackToPool();
    }
}
