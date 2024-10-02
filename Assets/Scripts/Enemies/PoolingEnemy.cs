using System.Collections.Generic;
using UnityEngine;

public class PoolingEnemy : Singleton<PoolingEnemy>
{
    [SerializeField] private Witch witchPrefab;
    [SerializeField] private MiniSpider miniSpiderPrefab;
    [SerializeField] private PumpkinDude pumpkinDudePrefab;
    [SerializeField] private Doc docPrefab;
    [SerializeField] private Bat batPrefab;

    private List<Witch> witchList;
    private List<MiniSpider> miniSpiderList;
    private List<PumpkinDude> pumpkinDudeList;
    private List<Doc> docList;
    private List<Bat> batList;

    protected override void Awake()
    {
        base.Awake();

        witchList = new List<Witch>();
        miniSpiderList = new List<MiniSpider>();
        pumpkinDudeList = new List<PumpkinDude>();
        docList = new List<Doc>();
        batList = new List<Bat>();
    }

    public void SpawnEnemy(ETag _nameEnemy, Vector2 _position)
    {
        switch (_nameEnemy)
        {
            case ETag.Bat:
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
                if (witchList.Count == 0)
                {
                    Witch _newWitch = Instantiate(witchPrefab, _position, Quaternion.identity);
                    witchList.Add(_newWitch);
                }

                Witch _witch = witchList[0];
                witchList.RemoveAt(0);
                _witch.Born(_position);
                break;
            default:
                break;
        }
    }

    public void BackToPool(AEnemy oldEnemy)
    {
        switch (oldEnemy)
        {
            case Witch _witch:
                witchList.Add(_witch);
                break;
            case MiniSpider _miniSpider:
                miniSpiderList.Add(_miniSpider);
                break;
            case PumpkinDude _pumpkinDude:
                pumpkinDudeList.Add(_pumpkinDude);
                break;
            case Doc _doc:
                docList.Add(_doc);
                break;
            case Bat _bat:
                batList.Add(_bat);
                break;
            default:
                break;
        }
    }
}
