using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class AWeapon : MonoBehaviour
{
    [SerializeField] protected float numberBulletCanShootIn1Second;
    [SerializeField] protected ELayer targetLayer;
    protected float timeShoot;
    protected Vector3 aimDirection;
    protected bool isShooting;
    protected Vector2 mousePointPosition;

    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        spriteRenderer.sortingOrder = 1;

        timeShoot = 1 / numberBulletCanShootIn1Second;
        isShooting = false;
    }

    protected virtual void Update()
    {
        FlipAndRotateSpriteFollowMouse();

        if (!isShooting && (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)))
        {
            StartCoroutine(Shooting());
        }
    }

    protected virtual IEnumerator Shooting()
    {
        isShooting = true;
        PoolingBullet.Instance.ShootAk47Bullet(transform, mousePointPosition, targetLayer);

        yield return new WaitForSeconds(timeShoot);
        isShooting = false;
    }

    protected virtual Vector2 GetDirectionFollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        aimDirection = (mousePosition - transform.position).normalized;

        return (Vector2)aimDirection;
    }

    protected void FlipAndRotateSpriteFollowMouse()
    {
        // A(x,y) - Player Position, B(x1,y1) - Mouse Position, C(x1,y) 
        Vector3 a = transform.position;
        Vector3 b = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePointPosition = (Vector2)b;
        Vector3 c = new Vector3(b.x, a.y, 0);
        a.z = 0;
        b.z = 0;

        float angle = Vector2.Angle((Vector2)(b - a), (Vector2)(c - a));
        if (b.y < a.y)
        {
            angle = -angle;
        }

        if (b.x < a.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

    }
}
