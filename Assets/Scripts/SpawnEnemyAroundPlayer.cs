using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyAroundPlayer : Singleton<SpawnEnemyAroundPlayer>
{
    [SerializeField] private float radius;
    [SerializeField] private float timeToReSpawn;
    private float countTime = 0;
    private int level;

    private int currentNumberBat;
    private int maxNumberBat;

    private int currentNumberBanditNecromancer;
    private int maxNumberBanditNecromancer;

    private int currentNumberDoc;
    private int maxNumberDoc;

    private int currentNumberMiniSpider;
    private int maxNumberMiniSpider;

    private int currentNumberMinotaur;
    private int maxNumberMinotaur;

    private int currentNumberPinkSnake;
    private int maxNumberPinkSnake;

    private int currentNumberPumpkinDude;
    private int maxNumberPumpkinDude;

    private int currentNumberWitch;
    private int maxNumberWitch;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        level = 0;
    }

    private void FixedUpdate()
    {
        countTime += Time.fixedDeltaTime;

        if (countTime >= timeToReSpawn)
        {
            Spawn();
        }
    }

    private Vector3 GetRandomPositionAroundCenter()
    {
        Vector3 center = PlayerController.Instance.transform.position;
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        float x = center.x + Mathf.Cos(randomAngle) * radius;
        float y = center.y + Mathf.Sin(randomAngle) * radius;

        return new Vector3(x, y, 0);
    }

    public void Spawn()
    {
        if (currentNumberBat < maxNumberBat)
        {

        }
        PoolingEnemy.Instance.SpawnEnemy(ETag.Bat, GetRandomPositionAroundCenter());
        PoolingEnemy.Instance.SpawnEnemy(ETag.MiniSpider, GetRandomPositionAroundCenter());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
