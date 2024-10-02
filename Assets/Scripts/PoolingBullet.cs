using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingBullet : Singleton<PoolingBullet>
{
    [SerializeField] Ak47Bullet ak47BulletPrefab;
    [SerializeField] Silk silkPrefab;
    [SerializeField] GreenExplosion greenExplostionPrefab;
    [SerializeField] RedExplosion redExplostionPrefab;
    [SerializeField] Burn burnPrefab;

    protected List<Ak47Bullet> listAk47Bullet;
    protected List<Silk> listSilk;
    protected List<GreenExplosion> listGreenExplostion;
    protected List<RedExplosion> listRedExplostion;
    protected List<Burn> listBurn;

    protected override void Awake()
    {
        base.Awake();

        listAk47Bullet = new List<Ak47Bullet>();
        listSilk = new List<Silk>();
        listGreenExplostion = new List<GreenExplosion>();
        listRedExplostion = new List<RedExplosion>();
        listBurn = new List<Burn>();
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
            GreenExplosion _newGreenExplosion = Instantiate(greenExplostionPrefab, source.position, source.rotation);
            listGreenExplostion.Add(_newGreenExplosion);
        }

        GreenExplosion _greenExplosion = listGreenExplostion[0];
        listGreenExplostion.RemoveAt(0);
        _greenExplosion.StartShooting(source, Vector2.zero ,targetLayer);
    }

    public void ShootRedExplosion(Transform source, ELayer targetLayer)
    {
        if (listRedExplostion.Count == 0)
        {
            RedExplosion _newRedExplosion = Instantiate(redExplostionPrefab,source.position, source.rotation);
            listRedExplostion.Add(_newRedExplosion);
        }

        RedExplosion _redExplosion = listRedExplostion[0];
        listRedExplostion.RemoveAt(0);
        _redExplosion.StartShooting(source , Vector2.zero ,targetLayer);
    }

    public void ShootBurn(GameObject gameObject)
    {
        if (listBurn.Count == 0)
        {
            Burn _newBurn = Instantiate(burnPrefab, gameObject.transform.position, gameObject.transform.rotation);
            listBurn.Add(_newBurn);
        }

        Burn _burn = listBurn[0];
        listBurn.RemoveAt(0);
        _burn.StartShooting(gameObject);
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
            case GreenExplosion _oldGreenExplosionBullet:
                listGreenExplostion.Add(_oldGreenExplosionBullet);
                break;
            case RedExplosion _oldRedExplosionBullet:
                listRedExplostion.Add(_oldRedExplosionBullet);
                break;
            case Burn _oldBurn:
                listBurn.Add(_oldBurn);
                break;
            default:
                break;
        }
    }
}
