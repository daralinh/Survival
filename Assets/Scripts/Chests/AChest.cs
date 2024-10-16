using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public abstract class AChest : MonoBehaviour
{
    [SerializeField] public float displayTime;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected Rigidbody2D rb2D;
    protected CircleCollider2D circleCollider2D;

    private float countDisplayTime;

    protected virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Chest");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        rb2D.freezeRotation = true;
        rb2D.gravityScale = 0;
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        circleCollider2D.isTrigger = true;

        gameObject.SetActive(false);
    }

    public virtual void Born(Vector2 _positionToBorn)
    {
        transform.position = _positionToBorn;
        countDisplayTime = 0;
        gameObject.SetActive(true);
        circleCollider2D.enabled = true;
        spriteRenderer.flipX = (transform.position.x < PlayerController.Instance.transform.position.x) ? true : false;
    }

    public void FixedUpdate()
    {
        countDisplayTime += Time.fixedDeltaTime;

        if (Vector2.Distance(PlayerController.Instance.transform.position, transform.position) > 12)
        {
            BackToPool();
            return;
        }

        if (countDisplayTime >= displayTime)
        {
            countDisplayTime = 0;
            BackToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            OpenChest();
        }
    }

    protected virtual void OpenChest()
    {
        MusicManager.Instance.PlayChestSource(EMusic.OpenChest);
        animator.SetTrigger(EAnimation.Open.ToString());
        circleCollider2D.enabled = false;
    }

    protected virtual void BackToPool()
    {
        animator.SetTrigger(EAnimation.Idle.ToString());
        gameObject.SetActive(false);
        PoolingChest.Instance.BackToBool(this);
    }
}
