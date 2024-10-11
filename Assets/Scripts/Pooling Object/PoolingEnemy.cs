using System.Collections;
using System.Collections.Generic;
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

    private List<Witch> witchList = new List<Witch>();
    private List<MiniSpider> miniSpiderList = new List<MiniSpider>();
    private List<PumpkinDude> pumpkinDudeList = new List<PumpkinDude>();
    private List<Doc> docList = new List<Doc>();
    private List<Bat> batList = new List<Bat>();
    private List<PinkSnake> pinkSnakesList = new List<PinkSnake>();
    private List<Minotaur> minotaurList = new List<Minotaur>();
    private List<BanditNecromancer> banditNecromancerList = new List<BanditNecromancer>();
    private List<Dummy> dummyList = new List<Dummy>();

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

               if (banditNecromancerList.Count == 0)
               {
                    BanditNecromancer _newBanditNecromancer = Instantiate(banditNecromancerPrefab, _position, Quaternion.identity);
                    banditNecromancerList.Add(_newBanditNecromancer);
               }

                BanditNecromancer _banditNecromancer = banditNecromancerList[0];
                banditNecromancerList.RemoveAt(0);
                _banditNecromancer.Born(_position);
                break;

            case ETag.Minotaur:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberMinotaur >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberMinotaur++;

                if (minotaurList.Count == 0)
                {
                    Minotaur _newMinotaur = Instantiate(minotaurPrefab, _position, Quaternion.identity);
                    minotaurList.Add(_newMinotaur);
                }

                Minotaur _minotaur = minotaurList[0];
                minotaurList.RemoveAt(0);
                _minotaur.Born(_position);
                break;

            case ETag.PinkSnake:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberPinkSnake >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberPinkSnake++;

                if (pinkSnakesList.Count == 0)
                {
                    PinkSnake _newPinkSnake = Instantiate(pinkSnakePrefab, _position, Quaternion.identity);
                    pinkSnakesList.Add(_newPinkSnake);
                }

                PinkSnake _pinkSnake = pinkSnakesList[0];
                pinkSnakesList.RemoveAt(0);
                _pinkSnake.Born(_position);
                break;

            case ETag.Bat:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberBat >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberBat++;

                if (batList.Count == 0)
                {
                    Bat _newBat = Instantiate(batPrefab, _position, Quaternion.identity);
                    batList.Add(_newBat);
                }

                Bat _bat = batList[0];
                batList.RemoveAt(0);
                _bat.Born(_position);
                break;

            case ETag.MiniSpider:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberMiniSpider >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberMiniSpider++;

                if (miniSpiderList.Count == 0)
                {
                    MiniSpider _newMiniSpider = Instantiate(miniSpiderPrefab, _position, Quaternion.identity);
                    miniSpiderList.Add(_newMiniSpider);
                }

                MiniSpider _miniSpider = miniSpiderList[0];
                miniSpiderList.RemoveAt(0);
                _miniSpider.Born(_position);
                break;

            case ETag.PumpkinDude:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberPumpkinDude >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberPumpkinDude++;

                if (pumpkinDudeList.Count == 0)
                {
                    PumpkinDude _newPumpkinDude = Instantiate(pumpkinDudePrefab, _position, Quaternion.identity);
                    pumpkinDudeList.Add(_newPumpkinDude);
                }

                PumpkinDude _pumpkinDude = pumpkinDudeList[0];
                pumpkinDudeList.RemoveAt(0);
                _pumpkinDude.Born(_position);
                break;

            case ETag.Doc:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberDoc >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberDoc++;

                if (docList.Count == 0)
                {
                    Doc _newDoc = Instantiate(docPrefab, _position, Quaternion.identity);
                    docList.Add(_newDoc);
                }

                Doc _doc = docList[0];
                docList.RemoveAt(0);
                _doc.Born(_position);
                break;

            case ETag.Witch:
                if (SpawnEnemyAroundPlayer.Instance.CurrentNumberWitch >= LevelManager.Instance.GetMaxNumberEnemy(_nameEnemy))
                {
                    return;
                }

                SpawnEnemyAroundPlayer.Instance.CurrentNumberWitch++;

                if (witchList.Count == 0)
                {
                    Witch _newWitch = Instantiate(witchPrefab, _position, Quaternion.identity);
                    witchList.Add(_newWitch);
                }

                Witch _witch = witchList[0];
                witchList.RemoveAt(0);
                _witch.Born(_position);
                break;

            case ETag.Dummy:
                if (dummyList.Count == 0)
                {
                    Dummy _newDummy = Instantiate(dummyPrefab, _position, Quaternion.identity);
                    dummyList.Add(_newDummy);
                }

                Dummy _dummy = dummyList[0];
                dummyList.RemoveAt(0);
                _dummy.Born(_position);
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
                if (!minotaurList.Contains(_minotaur))
                {
                    SpawnEnemyAroundPlayer.Instance.CurrentNumberMinotaur--;
                    minotaurList.Add(_minotaur);
                }
                break;

            case PinkSnake _pinkSnake:
                if (!pinkSnakesList.Contains(_pinkSnake))
                {
                    SpawnEnemyAroundPlayer.Instance.CurrentNumberPinkSnake--;
                    pinkSnakesList.Add(_pinkSnake);
                }
                break;

            case Witch _witch:
                if (!witchList.Contains(_witch))
                {
                    SpawnEnemyAroundPlayer.Instance.CurrentNumberWitch--;
                    witchList.Add(_witch);
                }
                break;

            case MiniSpider _miniSpider:
                if (!miniSpiderList.Contains(_miniSpider))
                {
                    SpawnEnemyAroundPlayer.Instance.CurrentNumberMiniSpider--;
                    miniSpiderList.Add(_miniSpider);
                }
                break;

            case PumpkinDude _pumpkinDude:
                if (!pumpkinDudeList.Contains(_pumpkinDude))
                {
                    SpawnEnemyAroundPlayer.Instance.CurrentNumberPumpkinDude--;
                    pumpkinDudeList.Add(_pumpkinDude);
                }
                break;

            case Doc _doc:
                if (!docList.Contains(_doc))
                {
                    SpawnEnemyAroundPlayer.Instance.CurrentNumberDoc--;
                    docList.Add(_doc);
                }
                break;

            case Bat _bat:
                if (!batList.Contains(_bat))
                {
                    SpawnEnemyAroundPlayer.Instance.CurrentNumberBat--;
                    batList.Add(_bat);
                }
                break;

            default:
                break;
        }
    }
}
