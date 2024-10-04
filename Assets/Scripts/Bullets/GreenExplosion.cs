using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class GreenExplosion : ABullet
{
    [SerializeField] private float explosionRadius;
    [Header("-- slow effect --")]
    [SerializeField] private float timeToSlow;
    [SerializeField] private float percentSpeedReduction;

    private SlowEffect slowEffect;

    private Animator animator;

    protected override void Born()
    {
        tag = ETag.GreenExplosion.ToString();
        animator = GetComponent<Animator>();
        capsuleCollider.enabled = false;
        slowEffect = new SlowEffect(percentSpeedReduction, timeToSlow);
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

        foreach (Collider2D _object in objectsHit)
        {
            AHpManager hpManager
                            = _object.gameObject.GetComponents<Component>().FirstOrDefault(c => c is AHpManager) as AHpManager;
            if (hpManager != null)
            {
                hpManager.TakeDMG(dmg, transform.position);
                hpManager.TakeEffect(slowEffect);
            }
        }
    }

    public void ShootEvent()
    {
        animator.SetTrigger(EAnimation.Idle.ToString());
        gameObject.SetActive(false);
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