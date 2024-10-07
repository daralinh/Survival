using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditNecromancer : AEnemy
{
    [SerializeField] private float coolDownSkill;
    private float countCoolDownSkill;
    [Header("--- skill ---")]
    [SerializeField] private int numberOfBatToSpawn;
    [SerializeField] private float radiusSkill;

    public override void Born(Vector2 _position)
    {
        countCoolDownSkill = coolDownSkill;
        base.Born(_position);
    }

    protected override void Update()
    {
        countCoolDownSkill += Time.deltaTime;
        base.Update();
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

        if (countAttackTime >= 1 / attackSpeed && Vector2.Distance(capsuleCollider2D.bounds.center, PlayerController.Instance.transform.position) <= attackRange)
        {
            ChangeStateToAttack();
        }
        else
        {
            if (countCoolDownSkill >= coolDownSkill)
            {
                ChangeStateToAttack();
            }
        }
    }

    public override void ExitAttackState()
    {
        if (Vector2.Distance(capsuleCollider2D.bounds.center, PlayerController.Instance.transform.position) <= attackRange)
        {
            PlayerController.Instance.HpManager.TakeDMG(dmg, transform.position);
        }

        if (countCoolDownSkill >= coolDownSkill)
        {
            SpawnInCircle.Instance.Spawn(ETag.Bat, numberOfBatToSpawn, radiusSkill, transform.position);

            countCoolDownSkill = 0;
        }

        base.ExitAttackState();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere(capsuleCollider2D.bounds.center, radiusSkill);
    }
}
