using System.Collections.Generic;
using UnityEngine;

public class MiniSpider : AEnemy
{
    [SerializeField] private float skillRange;
    private int canUseSkill;

    protected override void Awake()
    {
        base.Awake();
        canUseSkill = 0;
    }

    // Attack State
    public override void CheckAttackRange()
    {
        countAttackTime += Time.fixedDeltaTime;

        if (canUseSkill == 0 && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= skillRange)
        {
            UseSkill();
            return;
        }

        if (countAttackTime >= 1 / attackSpeed && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= attackRange)
        {
            ChangeStateToAttack();
        }
    }

    public override void EnterAttackState()
    {
        base.EnterAttackState();
    }

    public override void ExitAttackState()
    {
        if (canUseSkill == 1)
        {
            canUseSkill++;
            PoolingBullet.Instance.ShootSilk(transform, PlayerController.Instance.transform.position, ELayer.Player);
        }
        else
        {
            PlayerController.Instance.HpManager.TakeDMG(dmg, transform.position);
        }

        base.ExitAttackState();
    }

    public void UseSkill()
    {
        canUseSkill++;
        ChangeStateToAttack();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere(transform.position, skillRange);
    }
}
