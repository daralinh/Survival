using UnityEngine;

public class BulletCCannon : ABullet
{
    [SerializeField] protected TrailRenderer trailRenderer;
    
    protected override void Born()
    {
        tag = ETag.BulletCCannon.ToString();
        trailRenderer.emitting = false;
        base.Born();
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
