using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class ABullet : MonoBehaviour
{
    [SerializeField] protected float dmg;
    [SerializeField] protected float originSpeed;
    [SerializeField] protected float timeToHide;
    protected float speed;
    protected Vector2 moveDir;
    protected AWeapon fromWeapon;
    protected float countTimeToHide;
    protected bool isShooting;
    protected ELayer targetLayer;
    protected Transform sourceTransform;

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb2D;
    protected CapsuleCollider2D capsuleCollider;

    protected virtual void Awake()
    {
        tag = ETag.WeaponBullet.ToString();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        spriteRenderer.sortingOrder = 2;
        rb2D.freezeRotation = true;
        rb2D.gravityScale = 0;
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        capsuleCollider.isTrigger = true;

        speed = originSpeed;
        moveDir = Vector2.zero;
        countTimeToHide = 0;
        isShooting = false;
    }

    protected virtual void FixedUpdate()
    {
        if (isShooting)
        {
            rb2D.MovePosition(rb2D.position + moveDir * speed * Time.fixedDeltaTime);

            countTimeToHide += Time.fixedDeltaTime;

            if (countTimeToHide > timeToHide)
            {
                isShooting = false;
                BackToPool();
            }
        }
    }

    public virtual void StartShooting(Transform source, Vector2 targetPosition, ELayer _targetLayer)
    {
        sourceTransform = source;
        gameObject.transform.position = source.position;
        gameObject.transform.rotation = source.rotation;
        moveDir = (targetPosition - rb2D.position).normalized;
        targetLayer = _targetLayer;
        isShooting = true;
        gameObject.SetActive(true);
    }

    protected virtual void BackToPool()
    {
        gameObject.SetActive(false);
        countTimeToHide = 0;
        PoolingBullet.Instance.BackToPool(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.layer == LayerMask.NameToLayer(targetLayer.ToString()))
        {
            AHpManager hpComponent = collision2D.gameObject.GetComponents<Component>().FirstOrDefault(c => c is AHpManager) as AHpManager;
           
            if (hpComponent != null)
            {
                hpComponent.TakeDMG(dmg, sourceTransform.position);
            }
        }
    }
}
