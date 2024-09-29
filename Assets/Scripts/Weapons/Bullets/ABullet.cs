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
    [SerializeField] protected TrailRenderer trailRenderer;
    [SerializeField] protected EEffectApplied effectApplied;
    protected float speed;
    protected Vector2 moveDir;
    protected AWeapon fromWeapon;
    protected float countTimeToHide;
    protected bool isShooting;

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
        rb2D.freezeRotation = true;
        rb2D.gravityScale = 0;
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        capsuleCollider.isTrigger = true;

        speed = originSpeed;
        moveDir = Vector2.zero;
        countTimeToHide = 0;
        isShooting = false;
        trailRenderer.emitting = false;
    }

    protected void FixedUpdate()
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

    public virtual void StartShooting(Vector2 direction)
    {
        transform.position = fromWeapon.gameObject.transform.position;
        transform.rotation = fromWeapon.gameObject.transform.rotation;
        moveDir = direction;
        isShooting = true;
        gameObject.SetActive(true);
        trailRenderer.emitting = true;
    }

    public void SetFromWeapon(AWeapon _fromWeapon)
    {
        fromWeapon = _fromWeapon;
    }

    protected virtual void BackToPool()
    {
        trailRenderer.emitting = false;
        trailRenderer.Clear();
        gameObject.SetActive(false);
        countTimeToHide = 0;

        fromWeapon.AddOldBullet(this);
    }

    protected void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Enemy"))
        {
            AHpManager hpComponent = _collision.gameObject.GetComponents<Component>().FirstOrDefault(c => c is AHpManager) as AHpManager;
            if (hpComponent != null)
            {
                hpComponent.TakeDMG(dmg, fromWeapon.transform.position, effectApplied);
            }
        }
    }
}
