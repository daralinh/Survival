using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class AWeapon : MonoBehaviour
{
    [SerializeField] protected ABullet bulletPrefab;
    [SerializeField] protected float numberBulletCanShootIn1Second;
    protected List<ABullet> listBullet;
    protected float timeShoot;
    protected Vector3 aimDirection;
    protected bool isShooting;

    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        tag = ETag.Weapon.ToString();
        spriteRenderer = GetComponent<SpriteRenderer>();
        listBullet = new List<ABullet>();

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

        if (listBullet.Count == 0)
        {
            SpawnNewBullet();
        }

        ABullet _bullet = listBullet[0];
        listBullet.RemoveAt(0);
        _bullet.gameObject.SetActive(true); // Make sure bullet is active
        _bullet.StartShooting(GetDirectionFollowMouse());

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

    protected void SpawnNewBullet()
    {
        ABullet newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        newBullet.SetFromWeapon(this);
        newBullet.gameObject.SetActive(false);

        listBullet.Add(newBullet);
    }

    public void AddOldBullet(ABullet oldbullet)
    {
        if (!listBullet.Contains(oldbullet))
        {
            listBullet.Add(oldbullet);
        }

        oldbullet.gameObject.SetActive(false);
    }

    protected void FlipAndRotateSpriteFollowMouse()
    {
        // A(x,y) - Player Position, B(x1,y1) - Mouse Position, C(x1,y) 
        Vector3 a = transform.position;
        Vector3 b = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
