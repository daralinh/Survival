using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingBullet : Singleton<PoolingBullet>
{
    [SerializeField] Ak47Bullet ak47BulletPrefab;
    [SerializeField] Silk silkPrefab;
    [SerializeField] Explosion greenExplostionPrefab;

    protected List<Ak47Bullet> listAk47Bullet;
    protected List<Silk> listSilk;
    protected List<Explosion> listGreenExplostion;

    protected override void Awake()
    {
        base.Awake();

        listAk47Bullet = new List<Ak47Bullet>();
        listSilk = new List<Silk>();
        listGreenExplostion = new List<Explosion>();
    }

    public void ShootAk47Bullet(Transform source, Vector2 targetPostion, ELayer targetLayer)
    {
        if (listAk47Bullet.Count == 0)
        {
            Ak47Bullet _newbullet = Instantiate(ak47BulletPrefab, source.position, source.rotation);
            listAk47Bullet.Add(_newbullet);
        }

        Ak47Bullet _bullet = listAk47Bullet[0];
        listAk47Bullet.RemoveAt(0);
        _bullet.StartShooting(source, targetPostion, targetLayer);
    }

    public void ShootSilk(Transform source, Vector2 targetPostion, ELayer targetLayer)
    {
        if (listSilk.Count == 0)
        {
            Silk _newSilk = Instantiate(silkPrefab, source.position, source.rotation);
            listSilk.Add(_newSilk);
        }

        Silk _silk = listSilk[0];
        listSilk.RemoveAt(0);
        _silk.StartShooting(source, targetPostion, targetLayer);
    }

    public void ShootGreenExplosion(Transform source, ELayer targetLayer)
    {
        if (listGreenExplostion.Count == 0)
        {
            Explosion _newGreenExplosion = Instantiate(greenExplostionPrefab, source.position, source.rotation);
            listGreenExplostion.Add(_newGreenExplosion);
        }

        Explosion _greenExplosion = listGreenExplostion[0];
        listGreenExplostion.RemoveAt(0);
        _greenExplosion.StartShooting(source, Vector2.zero ,targetLayer);
    }

    public void BackToPool(ABullet _oldBullet)
    {
        switch (_oldBullet)
        {
            case Ak47Bullet _oldAk47Bullet:
                listAk47Bullet.Add(_oldAk47Bullet);
                break;
            case Silk _oldsilk:
                listSilk.Add(_oldsilk);
                break;
            case Explosion _oldExplosionBullet:
                ExplosionClassification(_oldExplosionBullet);
                break;
            default:
                break;
        }
    }

    private void ExplosionClassification(Explosion _explosion)
    {
        switch (_explosion.tag)
        {
            case string tag when tag == ETag.GreenExplosion.ToString():
                listGreenExplostion.Add(_explosion);
                break;
            default:
                break;
        }
    }
}
