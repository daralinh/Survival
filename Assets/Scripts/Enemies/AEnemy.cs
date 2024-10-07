using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyHpManager))]
[RequireComponent(typeof(EnemyEffectManager))]

public abstract class AEnemy : MonoBehaviour
{
    protected static int id = 0;
    [SerializeField] protected float dmg;
    [SerializeField] protected float originSpeed;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    protected float currentSpeed;
    protected Vector2 moveDir;
    protected bool isFacingLeft;
    protected float countAttackTime;

    protected IStateEnemy currentState;
    protected MoveStateEnemy moveState = new MoveStateEnemy();
    protected AttackStateEnemy attackState = new AttackStateEnemy();
    protected TakeDMGStateEnemy takeDMGState = new TakeDMGStateEnemy();
    protected DeathStateEnemy deathState = new DeathStateEnemy();

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected Rigidbody2D rb2D;
    protected CapsuleCollider2D capsuleCollider2D;
    protected EnemyHpManager hpManager;

    public int ID { get; protected set; }
    public float DMG => dmg;

    protected virtual void Awake()
    {
        ID = id++;
        gameObject.layer = LayerMask.NameToLayer(ELayer.Enemy.ToString());

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        hpManager = GetComponent<EnemyHpManager>();

        spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        rb2D.freezeRotation = true;
        rb2D.gravityScale = 0;
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        capsuleCollider2D.isTrigger = true;

        attackRange = Mathf.Max(0.2f, attackRange);
        moveDir = Vector2.zero;
        isFacingLeft = false;
        gameObject.SetActive(false);
    }

    public virtual void Born(Vector2 _position)
    {
        transform.position = _position;
        countAttackTime = 1 / attackSpeed;
        currentState = moveState;

        gameObject.SetActive(true);
        hpManager.HealFullHp();
        currentState.EnterState(this);
    }

    protected virtual void Update()
    {
        countAttackTime += Time.deltaTime;
        currentState.Update(this);
    }

    protected virtual void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }

    public void ChoosePlayerDirection()
    {
        moveDir = (PlayerController.Instance.transform.position - transform.position).normalized;
    }

    public void FlipSpriteRenderFollowPlayer()
    {
        if (PlayerController.Instance.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            isFacingLeft = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            isFacingLeft = false;
        }
    }

    public void ChangeSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }

    protected void MoveFollowDirection()
    {
        rb2D.MovePosition(rb2D.position + moveDir * currentSpeed * Time.fixedDeltaTime);
    }

    // Move State
    public virtual void ChangeStateToMoveState()
    {
        currentState = moveState;
        currentState.EnterState(this);
    }

    public virtual void EnterMoveState()
    {
        currentSpeed = originSpeed;
        animator.SetTrigger(EAnimation.Run.ToString());
    }

    public virtual void ExitMoveState()
    {
    }

    public virtual void UpdateMoveState()
    {
        ChoosePlayerDirection();
        FlipSpriteRenderFollowPlayer();
        CheckAttackRange();
    }

    public virtual void FixedUpdateMoveState()
    {
        MoveFollowDirection();
    }

    protected virtual void CheckAttackRange()
    {
        if (Vector2.Distance(capsuleCollider2D.bounds.center, PlayerController.Instance.transform.position) <= 0.2)
        {
            currentSpeed = 0;
        }
        else
        {
            currentSpeed = originSpeed;
        }

        if (countAttackTime >= 1 / attackSpeed && Vector2.Distance(capsuleCollider2D.bounds.center, PlayerController.Instance.transform.position) <= attackRange)
        {
            ChangeStateToAttack();
        }
    }

    // Attack State
    public void ChangeStateToAttack()
    {
        currentState = attackState;
        currentState.EnterState(this);
    }

    public virtual void EnterAttackState()
    {
        countAttackTime = 0;
        animator.SetTrigger(EAnimation.Attack.ToString());
    }

    public virtual void ExitAttackState()
    {
        ChangeStateToMoveState();
    }

    public virtual void UpdateAttackState()
    {
    }

    public virtual void FixedUpdateAttackState()
    {
    }

    // Take DMG State
    public void TakeDMGHandler(Vector2 sourceDMG)
    {
        ChangeStateToTakeDMG();
    }

    public void ChangeStateToTakeDMG()
    {
        currentState = takeDMGState;
        currentState.EnterState(this);
    }

    public virtual void EnterTakeDMGState()
    {
        animator.SetTrigger(EAnimation.TakeDMG.ToString());
        hpManager.FlashSprite();
    }

    public virtual void ExitTakeDMGState()
    {
        ChangeStateToMoveState();
    }

    public virtual void UpdateTakeDMGState()
    {
    }

    public virtual void FixedUpdateTakeDMGState()
    {
    }

    // Death State
    public virtual void ChangeStateToDeathState()
    {
        currentState = deathState;
        currentState.EnterState(this);
    }

    public virtual void EnterDeathState()
    {
        animator.SetTrigger(EAnimation.Death.ToString());
        hpManager.FlashSprite();
    }

    public virtual void ExitDeathState()
    {
        animator.SetTrigger(EAnimation.Idle.ToString());
        gameObject.SetActive(false);
        PoolingEnemy.Instance.BackToPool(this);
    }

    public virtual void UpdateDeathState()
    {
    }

    public virtual void FixedUpdateDeathState()
    {
    }

    public virtual void DeathEvent()
    {
        currentState.ExitState(this);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Gizmos.DrawWireSphere(capsuleCollider2D.bounds.center, attackRange);
    }
}