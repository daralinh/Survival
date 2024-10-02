using System.Collections.Generic;
using UnityEngine;

public class PoolingEnemy : MonoBehaviour
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

    private void Awake()
    {
        witchList = new List<Witch>();
        miniSpiderList = new List<MiniSpider>();
        pumpkinDudeList = new List<PumpkinDude>();
        docList = new List<Doc>();
        batList = new List<Bat>();
    }

    public void SpawnEnemy(AEnemy _enemy)
    {
        switch (_enemy)
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
