using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiBullet : ABullet
{
    [SerializeField] private TrailRenderer trailRenderer;

    protected override void Born()
    {
        trailRenderer.emitting = false;
        trailRenderer.Clear();
        base.Born();
    }

    public override void StartShooting(Transform _source, Vector2 _targetPosition, ELayer _targetLayer)
    {
        sourceTransform = _source;
        gameObject.transform.position = _source.position;
        FlipAndRotateFollowTarget(_targetPosition);
        moveDir = (_targetPosition - rb2D.position).normalized;
        targetLayer = _targetLayer;
        isShooting = true;
        gameObject.SetActive(true);
        trailRenderer.emitting = true;
    }

    protected override void BackToPool()
    {
        trailRenderer.emitting = false;
        trailRenderer.Clear();
        base.BackToPool();
    }
}
