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

    protected Queue<BulletCCannon> bulletCCannonQueue = new Queue<BulletCCannon>();
    protected Queue<Silk> silkQueue = new Queue<Silk>();
    protected Queue<GreenExplosion> greenExplostionQueue = new Queue<GreenExplosion>();
    protected Queue<RedExplosion> redExplosionQueue = new Queue<RedExplosion>();
    protected Queue<Burn> burnQueue = new Queue<Burn>();
    protected Queue<Lightning> lightningQueue = new Queue<Lightning>();
    protected Queue<FireBall> fireBallQueue = new Queue<FireBall>();
    protected Queue<FireStorm> fireStormQueue = new Queue<FireStorm>();
    protected Queue<FireSpot> fireSpotQueue = new Queue<FireSpot>();
    protected Queue<KunaiBullet> kunaiBulletQueue = new Queue<KunaiBullet>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void BackToPool(ABullet _oldBullet)
    {
        switch (_oldBullet)
        {
            case KunaiBullet _oldKunaiBullet:
                kunaiBulletQueue.Enqueue(_oldKunaiBullet);
                break;

            case FireSpot _oldFireSpot:
                fireSpotQueue.Enqueue(_oldFireSpot);
                break;

            case BulletCCannon _oldBulletCCannon:
                bulletCCannonQueue.Enqueue(_oldBulletCCannon);
                break;

            case Silk _oldsilk:
                silkQueue.Enqueue(_oldsilk);
                break;

            case GreenExplosion _oldGreenExplosionBullet:
                greenExplostionQueue.Enqueue(_oldGreenExplosionBullet);
                break;

            case RedExplosion _oldRedExplosionBullet:
                redExplosionQueue.Enqueue(_oldRedExplosionBullet);
                break;

            case Burn _oldBurn:
                burnQueue.Enqueue(_oldBurn);
                break;

            case Lightning _oldLightning:
                lightningQueue.Enqueue(_oldLightning);
                break;

            case FireBall _oldFireBall:
                fireBallQueue.Enqueue(_oldFireBall);
                break;

            default:
                break;
        }
    }

    public void ShootKunai(Transform source, Vector2 targetPosition, ELayer targetLayer)
    {
        if (kunaiBulletQueue.Count == 0)
        {
            KunaiBullet _newKunaiBullet = Instantiate(kunaiBulletPrefab, source.position, Quaternion.identity);
            kunaiBulletQueue.Enqueue(_newKunaiBullet);
        }

        kunaiBulletQueue.Dequeue().StartShooting(source, targetPosition, targetLayer);
    }

    public void ShootFireSpot(Transform source, Vector2 targetPosition, ELayer targetLayer)
    {
        if (fireSpotQueue.Count == 0)
        {
            FireSpot _newFireSpot = Instantiate(fireSpotPrefab, source.position, Quaternion.identity);
            fireSpotQueue.Enqueue(_newFireSpot);
        }

        fireSpotQueue.Dequeue().StartShooting(source, targetPosition, targetLayer);
    }

    public void ShootFireStorm(Transform source, Vector2 targetPosition, ELayer targetLayer)
    {
        if (fireStormQueue.Count == 0)
        {
            FireStorm _newFireStorm = Instantiate(fireStormPrefab, source.position, Quaternion.identity);
            fireStormQueue.Enqueue(_newFireStorm);
        }

        fireStormQueue.Dequeue().StartShooting(source, targetPosition, targetLayer);
    }

    public void ShootBulletCCannon(Transform source, Vector2 targetPostion, ELayer targetLayer)
    {
        if (bulletCCannonQueue.Count == 0)
        {
            BulletCCannon _newbullet = Instantiate(bulletCCannonPrefab, source.position, source.rotation);
            bulletCCannonQueue.Enqueue(_newbullet);
        }

        bulletCCannonQueue.Dequeue().StartShooting(source, targetPostion, targetLayer);
    }

    public void ShootSilk(Transform source, Vector2 targetPostion, ELayer targetLayer)
    {
        if (silkQueue.Count == 0)
        {
            Silk _newSilk = Instantiate(silkPrefab, source.position, source.rotation);
            silkQueue.Enqueue(_newSilk);
        }

        silkQueue.Dequeue().StartShooting(source, targetPostion, targetLayer);
    }

    public void ShootGreenExplosion(Transform source, ELayer targetLayer)
    {
        if (greenExplostionQueue.Count == 0)
        {
            GreenExplosion _newGreenExplosion = Instantiate(greenExplostionPrefab, source.position, source.rotation);
            greenExplostionQueue.Enqueue(_newGreenExplosion);
        }

        greenExplostionQueue.Dequeue().StartShooting(source, Vector2.zero ,targetLayer);
    }

    public void ShootRedExplosion(Transform source, ELayer targetLayer)
    {
        if (redExplosionQueue.Count == 0)
        {
            RedExplosion _newRedExplosion = Instantiate(redExplostionPrefab,source.position, source.rotation);
            redExplosionQueue.Enqueue(_newRedExplosion);
        }

        redExplosionQueue.Dequeue().StartShooting(source , Vector2.zero ,targetLayer);
    }

    public void ShootBurn(GameObject gameObject)
    {
        if (burnQueue.Count == 0)
        {
            Burn _newBurn = Instantiate(burnPrefab, gameObject.transform.position, gameObject.transform.rotation);
            burnQueue.Enqueue(_newBurn);
        }

        burnQueue.Dequeue().StartShooting(gameObject);
    }

    public void ShootFireBall(Transform source, Vector2 targetPosition, ELayer targetLayer)
    {
        if (fireBallQueue.Count == 0)
        {
            FireBall _newFireBall = Instantiate(fireBallPrefab, source.position, source.rotation);
            fireBallQueue.Enqueue(_newFireBall);
        }

        fireBallQueue.Dequeue().StartShooting(source, targetPosition, targetLayer);
    }

    public void ShootLightning(Vector2 targetPosition, ELayer targetLayer)
    {
        if (lightningQueue.Count == 0)
        {
            Lightning _newLightning = Instantiate(lightningPrefab, targetPosition, Quaternion.identity);
            lightningQueue.Enqueue(_newLightning);
        }

        lightningQueue.Dequeue().StartShooting(transform, targetPosition, targetLayer);
    }
}
