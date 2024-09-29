using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public abstract class AEnemyBullet : MonoBehaviour
{
    [SerializeField] protected float dmg;
    [SerializeField] protected float originSpeed;
    [SerializeField] protected float timeToHide;
    protected float speed;
    protected Vector2 moveDir;
    protected AEnemy fromEnemy;
    protected float countTimeToHide;
    protected bool isShooting;

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb2D;
    protected CapsuleCollider2D capsuleCollider;

    protected virtual void Awake()
    {
        tag = ETag.EnemyBullet.ToString();
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
        transform.position = fromEnemy.gameObject.transform.position;
        moveDir = direction;
        isShooting = true;
        gameObject.SetActive(true);
    }

    public void SetFromEnemy(AEnemy _enemy)
    {
        fromEnemy = _enemy;
    }

    protected virtual void BackToPool()
    {
        gameObject.SetActive(false);
        countTimeToHide = 0;

        fromEnemy.AddOldEnemyBullet(this);
    }

    protected void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            AHpManager hpComponent = _collision.gameObject.GetComponents<Component>().FirstOrDefault(c => c is AHpManager) as AHpManager;
            if (hpComponent != null)
            {
                hpComponent.TakeDMG(dmg, fromEnemy.transform.position, EEffectApplied.None);
            }
        }
    }
}
