using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkSnake : AEnemy
{
    [SerializeField] protected float speedWhenAttack;
    private bool isAttacking;

    protected override void Awake()
    {
        tag = ETag.PinkSnake.ToString();
        base.Awake();
    }

    public override void Born(Vector2 _position)
    {
        isAttacking = false;
        base.Born(_position);
    }

    public override void EnterAttackState()
    {
        base.EnterAttackState();
        isAttacking = true;
        currentSpeed = speedWhenAttack;
    }

    public override void UpdateAttackState()
    {
    }

    public override void FixedUpdateAttackState()
    {
        MoveFollowDirection();
    }

    public override void ExitAttackState()
    {
        isAttacking = false;
        base.ExitAttackState();
    }

    private void OnTriggerExit2D(Collider2D collision2D)
    {
        if (isAttacking == true && collision2D.CompareTag("Player"))
        {
            PlayerController.Instance.HpManager.TakeDMG(dmg, transform.position);
        }
    }
}
