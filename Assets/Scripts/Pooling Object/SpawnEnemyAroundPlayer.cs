using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyAroundPlayer : Singleton<SpawnEnemyAroundPlayer>
{
    [SerializeField] private float radius;
    [SerializeField] private float timeToReSpawn;
    [Header("Spawn Dummy")] 
    public float timeToDummyDeath;
    [SerializeField] private float numberOfObjects;
    [SerializeField] private float radiusOfSpawnDummy;
    private float countTime = 0;

    public int CurrentNumberBat { get; set; }
    public int CurrentNumberBanditNecromancer { get; set; }
    public int CurrentNumberDoc { get; set; }
    public int CurrentNumberMiniSpider { get; set; }
    public int CurrentNumberMinotaur { get; set; }
    public int CurrentNumberPinkSnake { get; set; }
    public int CurrentNumberPumpkinDude { get; set; }
    public int CurrentNumberWitch { get; set; }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CurrentNumberBat = 0;
        CurrentNumberBanditNecromancer = 0;
        CurrentNumberDoc = 0;
        CurrentNumberMiniSpider = 0;
        CurrentNumberMinotaur = 0;
        CurrentNumberPinkSnake = 0;
        CurrentNumberPumpkinDude = 0;
        CurrentNumberWitch = 0;
    }

    private void FixedUpdate()
    {
        countTime += Time.fixedDeltaTime;

        if (countTime >= timeToReSpawn)
        {
            Spawn();
            countTime = 0;
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
        PoolingEnemy.Instance.SpawnEnemy(ETag.Bat, GetRandomPositionAroundCenter());
        PoolingEnemy.Instance.SpawnEnemy(ETag.Doc, GetRandomPositionAroundCenter());
        PoolingEnemy.Instance.SpawnEnemy(ETag.PumpkinDude, GetRandomPositionAroundCenter());
        PoolingEnemy.Instance.SpawnEnemy(ETag.BanditNecromancer, GetRandomPositionAroundCenter());
        PoolingEnemy.Instance.SpawnEnemy(ETag.PinkSnake, GetRandomPositionAroundCenter());
        PoolingEnemy.Instance.SpawnEnemy(ETag.MiniSpider, GetRandomPositionAroundCenter());
        PoolingEnemy.Instance.SpawnEnemy(ETag.Witch, GetRandomPositionAroundCenter());
        PoolingEnemy.Instance.SpawnEnemy(ETag.Minotaur, GetRandomPositionAroundCenter());
    }

    public void SpawnDummy()
    {
        StartCoroutine(SpawnDummyCoroutine());
    }

    private IEnumerator SpawnDummyCoroutine()
    {
        MusicManager.Instance.PlaySpawnDummySource(EMusic.SpawnDummy);
        float _angleStep = 360 / numberOfObjects;
        Vector2 _positionToSpawnDummy = PlayerController.Instance.transform.position;

        for (int i = 0; i < numberOfObjects; i++)
        {
            float _angle = _angleStep * i;
            float _angleInRad = Mathf.Deg2Rad * _angle;

            float x = _positionToSpawnDummy.x + Mathf.Cos(_angleInRad) * radiusOfSpawnDummy;
            float y = _positionToSpawnDummy.y + Mathf.Sin(_angleInRad) * radiusOfSpawnDummy;
            Vector3 _position = new Vector3(x, y, 0);

            PoolingEnemy.Instance.SpawnEnemy(ETag.Dummy, _position);

            yield return new WaitForSeconds(0.05f);
        }

        MusicManager.Instance.PlaySpawnDummySource(EMusic.MarchingDummy);
        yield return new WaitForSeconds(58f);
        MusicManager.Instance.SpawnDummySource.Stop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radiusOfSpawnDummy);
    }
}
