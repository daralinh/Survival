using UnityEngine;

public class SpawnInCircle : Singleton<SpawnInCircle>
{
    public void Start()
    {
        //SpawnDummy();
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
}
