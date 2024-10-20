using System.Linq;
using UnityEngine;

public class KunaiBullet : ABullet
{
    [SerializeField] private TrailRenderer trailRenderer;
    private BezierMovement bezierMovement = new BezierMovement();

    protected override void Born()
    {
        trailRenderer.emitting = false;
        trailRenderer.Clear();
        base.Born();
    }

    protected override void FixedUpdate()
    {
        if (isShooting)
        {
            /*rb2D.MovePosition(rb2D.position + moveDir * speed * Time.fixedDeltaTime);

            countTimeToHide += Time.fixedDeltaTime;*/
            bezierMovement.MoveFollowCubicBezierPointFixedUpdate(transform, true);

            if (countTimeToHide > timeToHide || bezierMovement.inTheEnd)
            {
                isShooting = false;
                BackToPool();
            }
        }
    }

    public override void StartShooting(Transform _source, Vector2 _targetPosition, ELayer _targetLayer)
    {
        sourceTransform = _source;
        gameObject.transform.position = _source.position;
        //FlipAndRotateFollowTarget(_targetPosition);
        //moveDir = (_targetPosition - rb2D.position).normalized;
        targetLayer = _targetLayer;
        bezierMovement.Reset(transform.position, _targetPosition, speed);
        isShooting = true;
        gameObject.SetActive(true);
        trailRenderer.emitting = true;
    }

    protected override void OnTriggerEnter2D(Collider2D _collision2D)
    {
        if (_collision2D.gameObject.layer == LayerMask.NameToLayer(targetLayer.ToString()))
        {
            AHpManager _hpComponent
                = _collision2D.gameObject.GetComponents<Component>().FirstOrDefault(c => c is AHpManager) as AHpManager;

            if (_hpComponent != null)
            {
                _hpComponent.TakeDMG((targetLayer == ELayer.Enemy) ? dmg + UpgradeManager.Instance.BuffDMG : dmg, transform.position);
                BackToPool();
            }
        }
    }

    protected override void BackToPool()
    {
        trailRenderer.emitting = false;
        trailRenderer.Clear();
        base.BackToPool();
    }
}
