using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolingEnemy : Singleton<PoolingEnemy>
{
    [SerializeField] private Witch witchPrefab;
    [SerializeField] private MiniSpider miniSpiderPrefab;
    [SerializeField] private PumpkinDude pumpkinDudePrefab;
    [SerializeField] private Doc docPrefab;
    [SerializeField] private Bat batPrefab;
    [SerializeField] private PinkSnake pinkSnakePrefab;
    [SerializeField] private Minotaur minotaurPrefab;
    [SerializeField] private BanditNecromancer banditNecromancerPrefab;
    [SerializeField] private Dummy dummyPrefab;

    private Queue<Witch> witchQueue = new Queue<Witch>();
    private Queue<MiniSpider> miniSpiderQueue = new Queue<MiniSpider>();
    private Queue<PumpkinDude> pumpkinDudeQueue = new Queue<PumpkinDude>();
    private Queue<Doc> docQueue = new Queue<Doc>();
    private Queue<Bat> batQueue = new Queue<Bat>();
    private Queue<PinkSnake> pinkSnakesQueue = new Queue<PinkSnake>();
    private Queue<Minotaur> minotaurQueue = new Queue<Minotaur>();
    private Queue<BanditNecromancer> banditNecromancerQueue = new Queue<BanditNecromancer>();
    private Queue<Dummy> dummyQueue = new Queue<Dummy>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void SpawnEnemy(ETag _nameEnemy, Vector2 _position)
    {
        switch (_nameEnemy)
        {
            case ETag.BanditNecromancer:
               if (SpawnEnemyAroundPlayer.Instance.CurrentNumberBanditNecromancer >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
               {
                    return;
               }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberBanditNecromancer++;

               if (banditNecromancerQueue.Count == 0)
               {
                    BanditNecromancer _newBanditNecromancer = Instantiate(banditNecromancerPrefab, _position, Quaternion.identity);
                    banditNecromancerQueue.Enqueue(_newBanditNecromancer);
               }

                banditNecromancerQueue.Dequeue().Born(_position);
                break;

            case ETag.Minotaur:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberMinotaur >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberMinotaur++;

                if (minotaurQueue.Count == 0)
                {
                    Minotaur _newMinotaur = Instantiate(minotaurPrefab, _position, Quaternion.identity);
                    minotaurQueue.Enqueue(_newMinotaur);
                }

                minotaurQueue.Dequeue().Born(_position);
                break;

            case ETag.PinkSnake:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberPinkSnake >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberPinkSnake++;

                if (pinkSnakesQueue.Count == 0)
                {
                    PinkSnake _newPinkSnake = Instantiate(pinkSnakePrefab, _position, Quaternion.identity);
                    pinkSnakesQueue.Enqueue(_newPinkSnake);
                }

                pinkSnakesQueue.Dequeue().Born(_position);
                break;

            case ETag.Bat:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberBat >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberBat++;

                if (batQueue.Count == 0)
                {
                    Bat _newBat = Instantiate(batPrefab, _position, Quaternion.identity);
                    batQueue.Enqueue(_newBat);
                }

                batQueue.Dequeue().Born(_position);
                break;

            case ETag.MiniSpider:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberMiniSpider >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberMiniSpider++;

                if (miniSpiderQueue.Count == 0)
                {
                    MiniSpider _newMiniSpider = Instantiate(miniSpiderPrefab, _position, Quaternion.identity);
                    miniSpiderQueue.Enqueue(_newMiniSpider);
                }

                miniSpiderQueue.Dequeue().Born(_position);
                break;

            case ETag.PumpkinDude:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberPumpkinDude >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberPumpkinDude++;

                if (pumpkinDudeQueue.Count == 0)
                {
                    PumpkinDude _newPumpkinDude = Instantiate(pumpkinDudePrefab, _position, Quaternion.identity);
                    pumpkinDudeQueue.Enqueue(_newPumpkinDude);
                }

                pumpkinDudeQueue.Dequeue().Born(_position);
                break;

            case ETag.Doc:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberDoc >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberDoc++;

                if (docQueue.Count == 0)
                {
                    Doc _newDoc = Instantiate(docPrefab, _position, Quaternion.identity);
                    docQueue.Enqueue(_newDoc);
                }

                docQueue.Dequeue().Born(_position);
                break;

            case ETag.Witch:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberWitch >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberWitch++;

                if (witchQueue.Count == 0)
                {
                    Witch _newWitch = Instantiate(witchPrefab, _position, Quaternion.identity);
                    witchQueue.Enqueue(_newWitch);
                }

                witchQueue.Dequeue().Born(_position);
                break;

            case ETag.Dummy:
                if (dummyQueue.Count == 0)
                {
                    Dummy _newDummy = Instantiate(dummyPrefab, _position, Quaternion.identity);
                    dummyQueue.Enqueue(_newDummy);
                }

                dummyQueue.Dequeue().Born(_position);
                break;

            default:
                break;
        }
    }

    public void BackToPool(AEnemy oldEnemy)
    {
        PoolingItem.Instance.DropItem(oldEnemy.transform.position);

        switch (oldEnemy)
        {
            case Minotaur _minotaur:
                minotaurQueue.Enqueue(_minotaur);
                SpawnEnemyAroundPlayer.Instance.CurrentNumberMinotaur--;
                break;

            case PinkSnake _pinkSnake:
                pinkSnakesQueue.Enqueue(_pinkSnake);
                SpawnEnemyAroundPlayer.Instance.CurrentNumberPinkSnake--;
                break;

            case Witch _witch:
                witchQueue.Enqueue(_witch);
                SpawnEnemyAroundPlayer.Instance.CurrentNumberWitch--;
                break;

            case MiniSpider _miniSpider:
                miniSpiderQueue.Enqueue(_miniSpider);
                SpawnEnemyAroundPlayer.Instance.CurrentNumberMiniSpider--;
                break;

            case PumpkinDude _pumpkinDude:
                pumpkinDudeQueue.Enqueue(_pumpkinDude);
                SpawnEnemyAroundPlayer.Instance.CurrentNumberPumpkinDude--;
                break;

            case Doc _doc:
                docQueue.Enqueue(_doc);
                SpawnEnemyAroundPlayer.Instance.CurrentNumberDoc--;
                break;

            case Bat _bat:
                batQueue.Enqueue(_bat);
                SpawnEnemyAroundPlayer.Instance.CurrentNumberBat--;
                break;

            case BanditNecromancer _banditNecromancer:
                banditNecromancerQueue.Enqueue(_banditNecromancer);
                SpawnEnemyAroundPlayer.Instance.CurrentNumberBanditNecromancer--;
                break;

            default:
                break;
        }

        GameManager.Instance.IncNumberDeathEnemies();
    }
}
