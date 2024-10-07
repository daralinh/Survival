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
        gameObject.layer = LayerMask.NameToLayer(ELayer.Bullet.ToString());
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

        Born();
    } 

    protected virtual void Born()
    {
        gameObject.SetActive(false);
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

    public virtual void StartShooting(Transform _source, Vector2 _targetPosition, ELayer _targetLayer)
    {
        sourceTransform = _source;
        gameObject.transform.position = _source.position;
        gameObject.transform.rotation = _source.rotation;
        moveDir = (_targetPosition - rb2D.position).normalized;
        targetLayer = _targetLayer;
        isShooting = true;
        gameObject.SetActive(true);
    }

    protected virtual void BackToPool()
    {
        gameObject.SetActive(false);
        countTimeToHide = 0;
        transform.rotation = Quaternion.identity;
        PoolingBullet.Instance.BackToPool(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D _collision2D)
    {
        if (_collision2D.gameObject.layer == LayerMask.NameToLayer(targetLayer.ToString()))
        {
            AHpManager _hpComponent
                = _collision2D.gameObject.GetComponents<Component>().FirstOrDefault(c => c is AHpManager) as AHpManager;

            if (_hpComponent != null)
            {
                _hpComponent.TakeDMG(dmg, transform.position);
            }
        }
    }

    protected void FlipFollowTarget(Vector2 _targetPosition)
    {
        if (_targetPosition.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    protected void FlipAndRotateFollowTarget(Vector2 _targetPosition)
    {
        Vector2 a = transform.position;
        Vector2 b = _targetPosition;
        Vector2 c = new Vector2(b.x, a.y);
        float _angle = Vector2.Angle(b - a, c - a);

        if (b.y < a.y)
        {
            _angle = -_angle;
        }

        if (_targetPosition.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, _angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, _angle);
        }
    }
}
