using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class FireSpot : ABullet
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private float attackSpeed;
    private float countToAttack;

    private Vector2 explosionCenter;
    private Vector2 explosionSize;
    private CapsuleDirection2D explosionDirection;

    private Animator animator;

    protected override void Awake()
    {
        tag = ETag.FireSpot.ToString();
        animator = GetComponent<Animator>();
        base.Awake();
    }
    protected override void Born()
    {
        countToAttack = 1 / attackSpeed;
        base.Born();
        spriteRenderer.sortingOrder = 0;
    }

    protected void Update()
    {
        if (isShooting)
        {
            countToAttack += Time.deltaTime;

            if (countToAttack >= 1 / attackSpeed)
            {
                Hanlder();
                countToAttack = 0;
            }
        }
    }

    protected override void FixedUpdate()
    {
        if (isShooting)
        {
            countTimeToHide += Time.fixedDeltaTime;

            if (countTimeToHide > timeToHide)
            {
                isShooting = false;
                animator.SetTrigger(EAnimation.Death.ToString());
            }
        }
    }

    private void Hanlder()
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
                hpManager.TakeDMG(+UpgradeManager.Instance.BuffDMG, transform.position);
                //PoolingBullet.Instance.ShootBurn(hpManager.gameObject);
            }
        }
    }

    public override void StartShooting(Transform _source, Vector2 _targetPosition, ELayer _targetLayer)
    {
        sourceTransform = _source;
        gameObject.transform.position = _source.position;
        targetLayer = _targetLayer;
        isShooting = true;
        gameObject.SetActive(true);
        animator.SetTrigger(EAnimation.Shoot.ToString());
    }

    protected override void BackToPool()
    {
        animator.SetTrigger(EAnimation.Idle.ToString());
        base.BackToPool();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Gizmos.DrawWireSphere(capsuleCollider.bounds.center, explosionRadius);
    }
}
