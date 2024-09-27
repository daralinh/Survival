using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerHpManager))]

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

    IStatePlayerController currentState;
    MoveStatePlayerController moveState = new MoveStatePlayerController();
    TakeDMGStatePlayerController takeDMGState = new TakeDMGStatePlayerController();
    DeathStatePlayerController deathState = new DeathStatePlayerController();

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;
    private PlayerHpManager hpManager;

    public bool IsFacingLeft => isFacingLeft;

    protected override void Awake()
    {
        base.Awake();

        tag = ETag.Player.ToString();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        hpManager = GetComponent<PlayerHpManager>();

        spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        rb2D.freezeRotation = true;
        rb2D.gravityScale = 0;
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        capsuleCollider2D.isTrigger = true;
        trailRenderer.emitting = false;
    }

    private void Start()
    {
        currentState = moveState;
        currentState.EnterState(this);

        isDashing = false;
    }

    void Update()
    {
        currentState.Update(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }

    public void SetTriggerAnimation(EAnimation _name)
    {
        animator.SetTrigger(_name.ToString());
    }

    public void SetBoolAnimation(EAnimation _name, bool _value)
    {
        animator.SetBool(_name.ToString(), _value);
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

    // Move State
    public void ChangeStateToMoveState()
    {
        currentState = moveState;
        currentState.EnterState(this);
    }

    public void ChangeSpeedForMoveState()
    {
        currentSpeed = originMoveSpeed;
    }

    public void GetInputMove()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        if (moveDir != Vector2.zero)
        {
            SetTriggerAnimation(EAnimation.Run);
            GetInputDash();
        }
        else
        {
            SetTriggerAnimation(EAnimation.Idle);
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
    public void TakeDMGHandler(Vector2 sourceDMG, EEffectApplied eEffectApplied)
    {
        ChangeStateToTakeDMGState();
    }

    public void ChangeStateToTakeDMGState()
    {
        currentState = takeDMGState;
        currentState.EnterState(this);
    }

    public void EnterTakeDMGState()
    {
        animator.SetTrigger(EAnimation.TakeDMG.ToString());
        hpManager.FlashSprite();
    }

    public void ExitTakeDMGState()
    {
        ChangeSpeedForMoveState();
    }
}
