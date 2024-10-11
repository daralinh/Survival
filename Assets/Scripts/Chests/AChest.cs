using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public abstract class AChest : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected Rigidbody2D rb2D;
    protected CircleCollider2D circleCollider2D;

    protected virtual void Awake()
    {
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

        gameObject.SetActive(true);
        circleCollider2D.enabled = true;
        spriteRenderer.flipX = (transform.position.x < PlayerController.Instance.transform.position.x) ? true : false;
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
        Debug.Log("1");
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
