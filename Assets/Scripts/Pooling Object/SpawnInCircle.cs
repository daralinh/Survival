using UnityEngine;

public class SpawnInCircle : Singleton<SpawnInCircle>
{
    [Header("--- spawn dummy ---")]
    [SerializeField] private Dummy dummyPrefab;
    [SerializeField] private float numberOfObjects;
    [SerializeField] private float radius;

    public void Start()
    {
        //SpawnDummy();
    }

    public void SpawnDummy()
    {
        float _angleStep = 360 / numberOfObjects;
        
        for (int i = 0; i < numberOfObjects; i++)
        {
            float _angle = _angleStep * i;
            float _angleInRad = Mathf.Deg2Rad * _angle;

            float x = transform.position.x + Mathf.Cos(_angleInRad) * radius;
            float y = transform.position.y + Mathf.Sin(_angleInRad) * radius;
            Vector3 _position = new Vector3(x, y, 0);

            if (x > transform.position.x)
            {
                Dummy _dummy = Instantiate(dummyPrefab, _position, Quaternion.Euler(0, 0, 0));
                _dummy.Born(_dummy.transform.position);
            }
            else
            {
                Dummy _dummy = Instantiate(dummyPrefab, _position, Quaternion.Euler(0, -180, 0));
                _dummy.Born(_dummy.transform.position);
            }
        }
    }

    public void Spawn(ETag _tagEnemy, int _number, float _radius, Vector2 _position)
    {
        float angleStep = 360 / _number;

        for (int i = 0; i < _number; i++)
        {
            float angle = angleStep * i;
            float angleInRad = Mathf.Deg2Rad * angle;

            float x = _position.x + Mathf.Cos(angleInRad) * _radius;
            float y = _position.y + Mathf.Sin(angleInRad) * _radius;
            Vector3 _positionToSpawn = new Vector3(x, y, 0);

            PoolingEnemy.Instance.SpawnEnemy(_tagEnemy, _positionToSpawn);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
