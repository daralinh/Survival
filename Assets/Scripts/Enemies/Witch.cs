using UnityEngine;

public class Witch : AEnemy
{
    protected override void Awake()
    {
        tag = ETag.Witch.ToString();
        base.Awake();
    }

    public override void CheckAttackRange()
    {
        countAttackTime += Time.deltaTime;
        Debug.Log(countAttackTime + " " + Time.deltaTime);
        if (countAttackTime >= 1 / attackSpeed)
        {
            ChangeStateToAttack();
        }
    }

    public override void ExitAttackState()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= attackRange)
        {
            PlayerController.Instance.HpManager.TakeDMG(dmg, transform.position);
        }

        base.ExitAttackState(); 
    }
}
