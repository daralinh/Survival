using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dummy : AEnemy
{
    private float timeToDeath;
    private float countTimeToDeath;

    protected override void Awake()
    {
        ID = id++;
        gameObject.layer = LayerMask.NameToLayer(ELayer.Enemy.ToString());
        tag = ETag.Dummy.ToString();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        hpManager = GetComponent<EnemyHpManager>();

        spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        rb2D.freezeRotation = true;
        rb2D.gravityScale = 0;
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2D.velocity = Vector2.zero;
        rb2D.isKinematic = true;
        capsuleCollider2D.isTrigger = false;

        isFacingLeft = false;
        currentSpeed = 0;

        gameObject.SetActive(false);
    }

    public override void Born(Vector2 _position)
    {
        timeToDeath = SpawnEnemyAroundPlayer.Instance.timeToDummyDeath;
        countTimeToDeath = 0;

        base.Born(_position);
        if (_position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    protected override void OnDrawGizmosSelected()
    {
    }

    protected override void Update()
    {
        countTimeToDeath += Time.deltaTime;

        if (countTimeToDeath >= timeToDeath)
        {
            ChangeStateToDeathState();
            return;
        }

        base.Update();
    }

    // Move State
    public override void EnterMoveState()
    {
        animator.SetTrigger(EAnimation.Run.ToString());
    }

    public override void UpdateMoveState()
    {
    }

    public override void FixedUpdateMoveState()
    {
    }

    // Death State
    public override void EnterDeathState()
    {
        currentState.ExitState(this);
    }

    public override void ExitDeathState()
    {
        gameObject.SetActive(false);
        PoolingEnemy.Instance.BackToPool(this);
    }
}
