using UnityEngine;

public class Witch : AEnemy
{
    private int chooseEnemy;
    protected override void Awake()
    {
        chooseEnemy = 0;
        tag = ETag.Witch.ToString();
        base.Awake();
    }

    protected override void CheckAttackRange()
    {
        if (Vector2.Distance(capsuleCollider2D.bounds.center, PlayerController.Instance.transform.position) <= 0.2)
        {
            currentSpeed = 0;
        }
        else
        {
            currentSpeed = originSpeed;
        }

        if (countAttackTime >= 1 / attackSpeed)
        {
            ChangeStateToAttack();
        }
    }


    // Attack State
    public override void ExitAttackState()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= attackRange)
        {
            PlayerController.Instance.HpManager.TakeDMG(dmg, transform.position);
        }
        else
        {
            if (++chooseEnemy % 2 == 0)
            {
                PoolingEnemy.Instance.SpawnEnemy(ETag.Doc, transform.position);
            }
            else
            {
                PoolingEnemy.Instance.SpawnEnemy(ETag.PumpkinDude, transform.position);
            }
        }

        base.ExitAttackState(); 
    }
}
