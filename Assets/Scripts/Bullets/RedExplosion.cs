using System.Linq;
using UnityEngine;

public class RedExplosion : ABullet
{
    [SerializeField] private float explosionRadius;
    private Animator animator;

    protected override void Born()
    {
        tag = ETag.RedExplosion.ToString();
        animator = GetComponent<Animator>();
        capsuleCollider.enabled = false;
        base.Born();
    }

    protected override void FixedUpdate()
    {
    }

    public override void StartShooting(Transform source, Vector2 targetPosition, ELayer _targetLayer)
    {
        sourceTransform = source;
        gameObject.transform.position = source.position;
        targetLayer = _targetLayer;
        isShooting = true;
        gameObject.SetActive(true);
        animator.SetTrigger(EAnimation.Shoot.ToString());

        Collider2D[] objectsHit = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.GetMask(_targetLayer.ToString()));
        MusicManager.Instance.PlayBulletSFX(EMusic.RedExplosion);
        foreach (Collider2D _object in objectsHit)
        {
            AHpManager hpManager
                = _object.gameObject.GetComponents<Component>().FirstOrDefault(c => c is AHpManager) as AHpManager;

            if (hpManager != null)
            {
                hpManager.TakeDMG((targetLayer == ELayer.Enemy) ? dmg + UpgradeManager.Instance.BuffDMG : dmg, transform.position);
                PoolingBullet.Instance.ShootBurn(hpManager.gameObject);
            }
        }
    }

    public void ShootEvent()
    {
        animator.SetTrigger(EAnimation.Idle.ToString());
        BackToPool();
    }

    protected override void OnTriggerEnter2D(Collider2D collision2D)
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}