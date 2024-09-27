using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyHpManager))]

public abstract class AEnemy : MonoBehaviour
{
    [SerializeField] protected float originSpeed;
    [SerializeField] protected GameObject deathVFXPrefab;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    protected float currentSpeed;
    protected Vector2 moveDir;
    protected bool isFacingLeft;
    protected EnemyHpManager hpManager;
    protected GameObject deathVFX;
    protected bool canAttack;

    IStateEnemy currentState;
    MoveStateEnemy moveState = new MoveStateEnemy();
    AttackStateEnemy attackState = new AttackStateEnemy();
    TakeDMGStateEnemy takeDMGState = new TakeDMGStateEnemy();
    DeathStateEnemy deathState = new DeathStateEnemy();

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected Rigidbody2D rb2D;
    protected CapsuleCollider2D capsuleCollider2D;

    protected virtual void Awake()
    {
        tag = "Enemy";
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
        canAttack = true;
    }

    protected virtual void Update()
    {
        currentState.Update(this);
    }

    protected virtual void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }

    public abstract void AddOldEnemyBullet(AEnemyBullet _enemyBullet);

    protected abstract void SpawnNewBullet();

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

    // Move State
    public virtual void ChangeStateToMoveState()
    {
        currentState = moveState;
        currentState.EnterState(this);
    }

    public virtual void EnterMoveState()
    {
        currentSpeed = originSpeed;
        animator.SetBool(EAnimation.Run.ToString(), true);
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
        if (canAttack && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= attackRange)
        {
            ChangeStateToAttack();
        }
    }

    // Attack State
    public void ChangeStateToAttack()
    {
        if (!canAttack)
        {
            ChangeStateToMoveState();
            return;
        }

        canAttack = false;
        currentState = attackState;
        currentState.EnterState(this);
    }

    public virtual void EnterAttackState()
    {
        animator.SetBool(EAnimation.Run.ToString(), false);
        animator.SetTrigger(EAnimation.Attack.ToString());
    }

    public virtual void ExitAttackState()
    {
        Debug.Log("Attack to Move");
        StartCoroutine(WaitCoolDownAttack());
        ChangeStateToMoveState();
    }

    public virtual void UpdateAttackState()
    {
    }

    public void FixedUpdateAttackState()
    {
    }

    protected IEnumerator WaitCoolDownAttack()
    {
        yield return new WaitForSeconds(1/attackSpeed);
        canAttack = true;
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
        currentSpeed = 0;
        animator.SetBool(EAnimation.Run.ToString(), false);
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
        currentSpeed = 0;
        animator.SetBool(EAnimation.Run.ToString(), false);
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
        deathVFX.transform.position = transform.position;
        deathVFX.SetActive(true);
    }
}