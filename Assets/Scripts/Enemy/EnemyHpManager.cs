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
        Debug.Log("enemy take dmg  " + enemy.name);
        currentHp = Mathf.Max(currentHp - dmg, 0);
        
        if (currentHp > 0)
        {
            enemy.TakeDMGHandler(sourceDMG);
        }
        else
        {
            enemy.ChangeStateToDeathState();
        }
    }
}
