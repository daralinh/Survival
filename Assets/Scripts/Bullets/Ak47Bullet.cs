using UnityEngine;

public class Ak47Bullet : ABullet
{
    [SerializeField] protected TrailRenderer trailRenderer;

    protected override void Awake()
    {
        base.Awake();
        trailRenderer.emitting = false;
    }

    public override void StartShooting(Transform source, Vector2 targetPosition, ELayer targetLayer)
    {
        base.StartShooting(source, targetPosition, targetLayer);

        trailRenderer.emitting = true;
    }

    protected override void BackToPool()
    {
        trailRenderer.emitting = false;
        trailRenderer.Clear();
        base.BackToPool();
    }
}
