using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingChest : Singleton<PoolingChest>
{
    [SerializeField] private int rateDropGoldChest;
    [SerializeField] private NormalChest normalChestPrefab;
    [SerializeField] private GoldChest goldChestPrefab;

    private List<NormalChest> normalChestList = new List<NormalChest>();
    private List<GoldChest> goldChestList = new List<GoldChest>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void SpawnNormalChest(Vector2 _positionToSpawn)
    {
        if (normalChestList.Count == 0)
        {
            NormalChest _newNormalChest = Instantiate(normalChestPrefab, _positionToSpawn, Quaternion.identity);
            normalChestList.Add(_newNormalChest);
        }

        NormalChest normalChest = normalChestList[0];
        normalChestList.RemoveAt(0);
        normalChest.Born(_positionToSpawn);
    }

    public void SpawnGoldChest(Vector2 _positionToSpawn)
    {
        if (normalChestList.Count == 0)
        {
            NormalChest _newNormalChest = Instantiate(normalChestPrefab, _positionToSpawn, Quaternion.identity);
            normalChestList.Add(_newNormalChest);
        }

        NormalChest normalChest = normalChestList[0];
        normalChestList.RemoveAt(0);
        normalChest.Born(_positionToSpawn);
    }

    public void BackToBool(AChest oldChest)
    {
        switch (oldChest)
        {
            case NormalChest normalChest:
                if (!normalChestList.Contains(normalChest))
                {
                    normalChestList.Add(normalChest);
                }

                return;
            case GoldChest goldChest:
                if (!goldChestList.Contains(goldChest))
                {
                    goldChestList.Add(goldChest);
                }

                return;
            default:
                break;
        }
    }

    public void DropChest(Vector2 _positionToDrop)
    {
        int randomValue = Random.Range(1, 100);
        
        if (randomValue < rateDropGoldChest)
        {
            SpawnGoldChest(_positionToDrop);
            return;
        }

        SpawnNormalChest(_positionToDrop);
    }
}
