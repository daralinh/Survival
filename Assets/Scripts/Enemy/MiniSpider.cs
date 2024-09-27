using System.Collections.Generic;
using UnityEngine;

public class MiniSpider : AEnemy
{
    [SerializeField] private AEnemyBullet silkPrefab;
    [SerializeField] private float skillRange;
    protected List<AEnemyBullet> listEnemyBullet;
    private int canUseSkill;

    protected override void Awake()
    {
        base.Awake();
        listEnemyBullet = new List<AEnemyBullet>();
        canUseSkill = 0;
        SpawnNewBullet();
    }

    protected override void SpawnNewBullet()
    {
        AEnemyBullet _spiderSilk = Instantiate(silkPrefab, transform.position, Quaternion.identity);
        _spiderSilk.gameObject.SetActive(false);
        _spiderSilk.SetFromEnemy(this);
        listEnemyBullet.Add(_spiderSilk);
    }

    public override void AddOldEnemyBullet(AEnemyBullet _enemyBullet)
    {
        if (_enemyBullet is SpiderSilk && listEnemyBullet.Count == 1 && !listEnemyBullet.Contains(_enemyBullet))
        {
            listEnemyBullet.Add(_enemyBullet);
        }
    }

    // Attack State
    public override void CheckAttackRange()
    {
        if (canUseSkill == 0 && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= skillRange)
        {
            UseSkill();
            return;
        }

        if (canAttack && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= attackRange)
        {
            ChangeStateToAttack();
        }
    }

    public override void EnterAttackState()
    {
        ChoosePlayerDirection();
        FlipSpriteRenderFollowPlayer();
        base.EnterAttackState();
    }

    public override void ExitAttackState()
    {
        if (canUseSkill == 1)
        {
            canUseSkill++;
            AEnemyBullet _spiderSilk = listEnemyBullet[0];
            _spiderSilk.StartShooting(moveDir);
        }

        base.ExitAttackState();
    }

    public void UseSkill()
    {
        canUseSkill++;
        ChangeStateToAttack();
    }
}
