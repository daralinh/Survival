using UnityEngine;

public class EnemyHpManager : AHpManager
{
    private AEnemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<AEnemy>();
    }

    public override void TakeDMG(float dmg, Vector2 sourceDMG)
    {
        base.TakeDMG(dmg, sourceDMG);
        
        if (currentHp > 0)
        {
            enemy.TakeDMGHandler(sourceDMG);
        }
        else
        {
            enemy.ChangeStateToDeathState();
        }
    }

    public override void TakeEffect(AEffect aEffect)
    {
    }
}
