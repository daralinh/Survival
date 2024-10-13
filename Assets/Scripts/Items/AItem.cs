using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public abstract class AItem : MonoBehaviour
{
    protected static int id = 0;

    [SerializeField] protected int exp;
    [SerializeField] protected float speed;
    [SerializeField] protected float displayTime;
    protected Vector2 moveDir;

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb2D;
    protected CircleCollider2D circleCollider2D;
    protected Animator animator;

    protected bool isMoving;
    protected float countDisplayTime;

    public int ID { get; protected set; }
    public int Exp => exp; 

    protected virtual void Awake()
    {
        ID = id++;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();

        spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        spriteRenderer.sortingOrder = 1;
        rb2D.freezeRotation = true;
        rb2D.gravityScale = 0;
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        circleCollider2D.isTrigger = true;

        gameObject.SetActive(false);
    }

    public virtual void Born(Vector2 _position)
    {
        countDisplayTime = 0;
        transform.position = _position;

        transform.rotation = (transform.position.x < PlayerController.Instance.transform.position.x) ?
            Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);

        isMoving = false;
        gameObject.SetActive(true);
        WaitToMove();
    }

    protected virtual void FixedUpdate()
    {
        if (isMoving)
        {
            ChooseDirection();
            rb2D.MovePosition(rb2D.position + moveDir * speed * Time.fixedDeltaTime);
        }

        if (Vector2.Distance(PlayerController.Instance.transform.position, transform.position) <= 0.2)
        {
            BackToBool();
        }

        countDisplayTime += Time.fixedDeltaTime;
        if (countDisplayTime >= displayTime)
        {
            isMoving = false;
            animator.SetTrigger(EAnimation.Idle.ToString());
            gameObject.SetActive(false);
            PoolingItem.Instance.BackToPool(this);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.CompareTag("Player"))
        {
            MoveToPlayer();
            return;
        }
    }

    protected virtual void MoveToPlayer()
    {
        circleCollider2D.enabled = false;
        animator.SetTrigger(EAnimation.Run.ToString());
        isMoving = true;
    }

    protected void ChooseDirection()
    {
        moveDir = (PlayerController.Instance.transform.position - transform.position).normalized;
    }

    protected virtual void BackToBool()
    {
        isMoving = false;
        animator.SetTrigger(EAnimation.Idle.ToString());
        gameObject.SetActive(false);
        PoolingItem.Instance.BackToPool(this);
        UpgradeManager.Instance.TakeExp(exp);
    }

    protected virtual void WaitToMove()
    {
        StartCoroutine(WaitHandler());
    }

    protected IEnumerator WaitHandler()
    {
        yield return new WaitForSeconds(0.25f);
        circleCollider2D.enabled = true;
    }
}
