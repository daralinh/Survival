using System.Collections.Generic;
using UnityEngine;

public class PoolingBullet : Singleton<PoolingBullet>
{
    [SerializeField] BulletCCannon bulletCCannonPrefab;
    [SerializeField] Silk silkPrefab;
    [SerializeField] GreenExplosion greenExplostionPrefab;
    [SerializeField] RedExplosion redExplostionPrefab;
    [SerializeField] Burn burnPrefab;
    [SerializeField] Lightning lightningPrefab;
    [SerializeField] FireBall fireBallPrefab;
    [SerializeField] FireStorm fireStormPrefab;
    [SerializeField] FireSpot fireSpotPrefab;
    [SerializeField] KunaiBullet kunaiBulletPrefab;

    protected List<BulletCCannon> bulletCCannonlist = new List<BulletCCannon>();
    protected List<Silk> silkList = new List<Silk>();
    protected List<GreenExplosion> greenExplostionList = new List<GreenExplosion>();
    protected List<RedExplosion> redExplosionList = new List<RedExplosion>();
    protected List<Burn> burnList = new List<Burn>();
    protected List<Lightning> lightningList = new List<Lightning>();
    protected List<FireBall> fireBallList = new List<FireBall>();
    protected List<FireStorm> fireStormList = new List<FireStorm>();
    protected List<FireSpot> fireSpotList = new List<FireSpot>();
    protected List<KunaiBullet> kunaiBulletList = new List<KunaiBullet>();

    protected override void Awake()
    {
        base.Awake();
    }
    public void BackToPool(ABullet _oldBullet)
    {
        switch (_oldBullet)
        {
            case KunaiBullet _oldKunaiBullet:
                kunaiBulletList.Add(_oldKunaiBullet);
                break;

            case FireSpot _oldFireSpot:
                fireSpotList.Add(_oldFireSpot);
                break;

            case BulletCCannon _oldBulletCCannon:
                bulletCCannonlist.Add(_oldBulletCCannon);
                break;

            case Silk _oldsilk:
                silkList.Add(_oldsilk);
                break;

            case GreenExplosion _oldGreenExplosionBullet:
                greenExplostionList.Add(_oldGreenExplosionBullet);
                break;

            case RedExplosion _oldRedExplosionBullet:
                redExplosionList.Add(_oldRedExplosionBullet);
                break;

            case Burn _oldBurn:
                burnList.Add(_oldBurn);
                break;

            case Lightning _oldLightning:
                lightningList.Add(_oldLightning);
                break;

            case FireBall _oldFireBall:
                fireBallList.Add(_oldFireBall);
                break;

            default:
                break;
        }
    }

    public void ShootKunai(Transform source, Vector2 targetPosition, ELayer targetLayer)
    {
        if (kunaiBulletList.Count == 0)
        {
            KunaiBullet _newKunaiBullet = Instantiate(kunaiBulletPrefab, source.position, Quaternion.identity);
            kunaiBulletList.Add(_newKunaiBullet);
        }

        KunaiBullet _kunaiBullet = kunaiBulletList[0];
        kunaiBulletList.RemoveAt(0);
        _kunaiBullet.StartShooting(source, targetPosition, targetLayer);
    }

    public void ShootFireSpot(Transform source, Vector2 targetPosition, ELayer targetLayer)
    {
        if (fireSpotList.Count == 0)
        {
            FireSpot _newFireSpot = Instantiate(fireSpotPrefab, source.position, Quaternion.identity);
            fireSpotList.Add(_newFireSpot);
        }

        FireSpot _fireSpot = fireSpotList[0];
        fireSpotList.RemoveAt(0);
        _fireSpot.StartShooting(source, targetPosition, targetLayer);
    }

    public void ShootFireStorm(Transform source, Vector2 targetPosition, ELayer targetLayer)
    {
        if (fireStormList.Count == 0)
        {
            FireStorm _newFireStorm = Instantiate(fireStormPrefab, source.position, Quaternion.identity);
            fireStormList.Add(_newFireStorm);
        }

        FireStorm _fireStorm = fireStormList[0];
        fireStormList.RemoveAt(0);
        _fireStorm.StartShooting(source, targetPosition, targetLayer);
    }

    public void ShootBulletCCannon(Transform source, Vector2 targetPostion, ELayer targetLayer)
    {
        if (bulletCCannonlist.Count == 0)
        {
            BulletCCannon _newbullet = Instantiate(bulletCCannonPrefab, source.position, source.rotation);
            bulletCCannonlist.Add(_newbullet);
        }

        BulletCCannon _bullet = bulletCCannonlist[0];
        bulletCCannonlist.RemoveAt(0);
        _bullet.StartShooting(source, targetPostion, targetLayer);
    }

    public void ShootSilk(Transform source, Vector2 targetPostion, ELayer targetLayer)
    {
        if (silkList.Count == 0)
        {
            Silk _newSilk = Instantiate(silkPrefab, source.position, source.rotation);
            silkList.Add(_newSilk);
        }

        Silk _silk = silkList[0];
        silkList.RemoveAt(0);
        _silk.StartShooting(source, targetPostion, targetLayer);
    }

    public void ShootGreenExplosion(Transform source, ELayer targetLayer)
    {
        if (greenExplostionList.Count == 0)
        {
            GreenExplosion _newGreenExplosion = Instantiate(greenExplostionPrefab, source.position, source.rotation);
            greenExplostionList.Add(_newGreenExplosion);
        }

        GreenExplosion _greenExplosion = greenExplostionList[0];
        greenExplostionList.RemoveAt(0);
        _greenExplosion.StartShooting(source, Vector2.zero ,targetLayer);
    }

    public void ShootRedExplosion(Transform source, ELayer targetLayer)
    {
        if (redExplosionList.Count == 0)
        {
            RedExplosion _newRedExplosion = Instantiate(redExplostionPrefab,source.position, source.rotation);
            redExplosionList.Add(_newRedExplosion);
        }

        RedExplosion _redExplosion = redExplosionList[0];
        redExplosionList.RemoveAt(0);
        _redExplosion.StartShooting(source , Vector2.zero ,targetLayer);
    }

    public void ShootBurn(GameObject gameObject)
    {
        if (burnList.Count == 0)
        {
            Burn _newBurn = Instantiate(burnPrefab, gameObject.transform.position, gameObject.transform.rotation);
            burnList.Add(_newBurn);
        }

        Burn _burn = burnList[0];
        burnList.RemoveAt(0);
        _burn.StartShooting(gameObject);
    }

    public void ShootFireBall(Transform source, Vector2 targetPosition, ELayer targetLayer)
    {
        if (fireBallList.Count == 0)
        {
            FireBall _newFireBall = Instantiate(fireBallPrefab, source.position, source.rotation);
            fireBallList.Add(_newFireBall);
        }

        FireBall _fireBall = fireBallList[0];
        fireBallList.RemoveAt(0);
        _fireBall.StartShooting(source, targetPosition, targetLayer);
    }

    public void ShootLightning(Vector2 targetPosition, ELayer targetLayer)
    {
        if (lightningList.Count == 0)
        {
            Lightning _newLightning = Instantiate(lightningPrefab, targetPosition, Quaternion.identity);
            lightningList.Add(_newLightning);
        }

        Lightning _lightning = lightningList[0];
        lightningList.RemoveAt(0);
        _lightning.StartShooting(transform, targetPosition, targetLayer);
    }
}
