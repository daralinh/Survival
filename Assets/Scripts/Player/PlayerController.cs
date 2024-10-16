using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerHpManager))]
[RequireComponent(typeof(PlayerEffectManager))]

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float originMoveSpeed;
    [SerializeField] private float buffSpeedWhenDash;
    [SerializeField] private float dashTime;
    [SerializeField] private float coolDownDash;
    [SerializeField] private TrailRenderer trailRenderer;
    private float currentSpeed;
    private Vector2 moveDir;
    private bool isDashing;
    private bool isFacingLeft;
    private bool canChangeSpeed;
    private Coroutine coroutineTakeDMG;

    IStatePlayerController currentState;
    MoveStatePlayerController moveState = new MoveStatePlayerController();
    TakeDMGStatePlayerController takeDMGState = new TakeDMGStatePlayerController();
    DeathStatePlayerController deathState = new DeathStatePlayerController();

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;
    private PlayerHpManager hpManager;
    private PlayerEffectManager effectManager;

    public bool IsFacingLeft => isFacingLeft;
    public PlayerHpManager HpManager => hpManager;

    private float oldSpeed;

    protected override void Awake()
    {
        base.Awake();

        gameObject.layer = LayerMask.NameToLayer(ELayer.Player.ToString());
        tag = ETag.Player.ToString();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        hpManager = GetComponent<PlayerHpManager>();
        effectManager = GetComponent<PlayerEffectManager>();

        spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        rb2D.freezeRotation = true;
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        trailRenderer.emitting = false;

        isDashing = false;
        canChangeSpeed = true;

        currentState = moveState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.Update(this);

        if (oldSpeed != currentSpeed)
        {
           // Debug.Log(oldSpeed + " " + currentSpeed);
            oldSpeed = currentSpeed;
        }
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }

    public void FlipSpriteFollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
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

    public void ReduceSpeedByPercent(float _valuePercent)
    {
        currentSpeed = Mathf.Max(0, currentSpeed - (currentSpeed * _valuePercent / 100));
    }

    public void BackToOriginSpeed()
    {
        currentSpeed = originMoveSpeed;
    }

    public void SetCanChangeSpeed(bool _value)
    {
        canChangeSpeed = _value;
    }

    // Move State
    public void ChangeStateToMoveState()
    {
        currentState = moveState;
        currentState.EnterState(this);
    }

    public void EnterMoveState()
    {
        animator.SetBool(EAnimation.Run.ToString(), true);

        if (canChangeSpeed)
        {
            currentSpeed = originMoveSpeed;
        }
    }

    public void UpdateMoveState()
    {
        GetInputMove();
        FlipSpriteFollowMouse();
    }

    public void FixedUpdateMoveState()
    {
        MoveFollowDirection();
    }
    
    public void GetInputMove()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        if (moveDir != Vector2.zero)
        {
            animator.SetTrigger(EAnimation.Run.ToString());
            GetInputDash();
        }
        else
        {
            animator.SetTrigger(EAnimation.Idle.ToString());
        }
    }

    public void MoveFollowDirection()
    {
        rb2D.MovePosition(rb2D.position + moveDir * currentSpeed * Time.fixedDeltaTime);
    }

    // Dash
    private void GetInputDash()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isDashing)
        {
            isDashing = true;
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        trailRenderer.emitting = true;
        currentSpeed *= buffSpeedWhenDash;
        yield return new WaitForSeconds(dashTime);

        currentSpeed /= buffSpeedWhenDash;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(Mathf.Max(coolDownDash - dashTime, 2));

        isDashing = false;
    }

    // Take DMG State
    public void TakeDMGHandler(Vector2 _sourceDMG)
    {
        ChangeStateToTakeDMGState();
    }

    public void ChangeStateToTakeDMGState()
    {
        if (currentState is TakeDMGStateEnemy)
        {
            Debug.Log("State take dmg");
            StopCoroutine(coroutineTakeDMG);
        }
        else
        {
            currentState = takeDMGState;
            currentState.EnterState(this);
        }
    }

    public void EnterTakeDMGState()
    {
        animator.SetBool(EAnimation.Run.ToString(), false);
        animator.SetTrigger(EAnimation.Idle.ToString());
        coroutineTakeDMG = StartCoroutine(CoroutineTakeDMG());
    }

    private IEnumerator CoroutineTakeDMG()
    {
        yield return new WaitForSeconds(0.1f);
        currentState.ExitState(this);
    }

    public void ExitTakeDMGState()
    {
        ChangeStateToMoveState();
    }
}
