using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Lightning : ABullet
{
    [SerializeField] private float explosionRadius;
    [Header("-- slow effect --")]
    [SerializeField] private float timeToSlow;
    [SerializeField] private float percentSpeedReduction;
    private Vector2 explosionCenter;
    private Vector2 explosionSize;
    private CapsuleDirection2D explosionDirection;
    private SlowEffect slowEffect;

    private Animator animator;

    protected override void Born()
    {
        tag = ETag.Lightning.ToString();
        slowEffect = new SlowEffect(percentSpeedReduction, timeToSlow);
        animator = GetComponent<Animator>();
        spriteRenderer.sortingOrder = 3;
        capsuleCollider.enabled = false;
        base.Born();
    }

    protected override void FixedUpdate()
    {
    }

    public override void StartShooting(Transform source, Vector2 targetPosition, ELayer _targetLayer)
    {
        transform.position = targetPosition;
        targetLayer = _targetLayer;
        gameObject.SetActive(true);
        animator.SetTrigger(EAnimation.Shoot.ToString());
    }

    public void ShootEvent()
    {
        explosionCenter = capsuleCollider.bounds.center;
        explosionSize = capsuleCollider.bounds.size;
        explosionDirection = capsuleCollider.direction;

        Collider2D[] objectsHit = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.GetMask(targetLayer.ToString()));

        foreach (Collider2D _object in objectsHit)
        {
            AHpManager hpManager
                = _object.gameObject.GetComponents<Component>().FirstOrDefault(c => c is AHpManager) as AHpManager;
            if (hpManager != null)
            {
                hpManager.TakeDMG((targetLayer == ELayer.Enemy) ? dmg + UpgradeManager.Instance.BuffDMG : dmg, transform.position);
                //PoolingBullet.Instance.ShootBurn(hpManager.gameObject);
            }
        }
    }

    protected override void BackToPool()
    {
        animator.SetTrigger(EAnimation.Idle.ToString());
        gameObject.SetActive(false);
        PoolingBullet.Instance.BackToPool(this);
    }

    protected override void OnTriggerEnter2D(Collider2D collision2D)
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Gizmos.DrawWireSphere(capsuleCollider.bounds.center, explosionRadius);
    }
}
