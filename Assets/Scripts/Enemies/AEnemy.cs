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
    [SerializeField] protected float dmg;
    [SerializeField] protected float originSpeed;
    [SerializeField] protected GameObject deathVFXPrefab;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    protected float currentSpeed;
    protected Vector2 moveDir;
    protected bool isFacingLeft;
    protected EnemyHpManager hpManager;
    protected GameObject deathVFX;
    protected float countAttackTime;

    protected IStateEnemy currentState;
    MoveStateEnemy moveState = new MoveStateEnemy();
    AttackStateEnemy attackState = new AttackStateEnemy();
    TakeDMGStateEnemy takeDMGState = new TakeDMGStateEnemy();
    DeathStateEnemy deathState = new DeathStateEnemy();

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected Rigidbody2D rb2D;
    protected CapsuleCollider2D capsuleCollider2D;

    public float DMG => dmg;

    protected virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer(ELayer.Enemy.ToString());
        tag = ETag.Enemy.ToString();
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

        isFacingLeft = false;
        currentState = moveState;
        currentState.EnterState(this);
        deathVFX = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        deathVFX.SetActive(false);
        countAttackTime = 1/attackSpeed;
    }

    protected virtual void Update()
    {
        currentState.Update(this);
    }

    protected virtual void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }

    public void ChoosePlayerDirection()
    {
        moveDir = ((Vector2)(PlayerController.Instance.transform.position - transform.position)).normalized;
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
        animator.SetBool(EAnimation.Run.ToString(), false);
    }

    public virtual void UpdateMoveState()
    {
        ChoosePlayerDirection();
        FlipSpriteRenderFollowPlayer();
        CheckAttackRange();
    }

    public virtual void FixedUpdateMoveState()
    {
        rb2D.MovePosition(rb2D.position + moveDir * currentSpeed * Time.fixedDeltaTime);
    }

    public virtual void CheckAttackRange()
    {
        countAttackTime += Time.deltaTime;

        if (countAttackTime >= 1/attackSpeed && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= attackRange)
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
        FlipSpriteRenderFollowPlayer();
    }

    public void FixedUpdateAttackState()
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
        gameObject.SetActive(false);
        deathVFX.SetActive(false);
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

    public virtual void DeathVFXEvent()
    {
        deathVFX.transform.position = transform.position;
        deathVFX.SetActive(true);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}