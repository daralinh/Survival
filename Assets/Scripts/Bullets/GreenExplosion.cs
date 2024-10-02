using UnityEngine;

[RequireComponent(typeof(Animator))]

public class GreenExplosion : ABullet
{
    [SerializeField] private float explosionRadius;
    private Animator animator;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        base.Awake();
        capsuleCollider.enabled = false;
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
            AHpManager hpManager = _object.GetComponent<AHpManager>();

            if (hpManager != null)
            {
                hpManager.TakeDMG(dmg, transform.position);
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
