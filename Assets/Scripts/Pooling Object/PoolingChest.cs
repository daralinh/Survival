using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingChest : Singleton<PoolingChest>
{
    [SerializeField] private int rateDropGoldChest;
    [SerializeField] private NormalChest normalChestPrefab;
    [SerializeField] private GoldChest goldChestPrefab;
    [SerializeField] private int maxQuantity;

    private Queue<NormalChest> normalChestQueue = new Queue<NormalChest>();
    private Queue<GoldChest> goldChestQueue = new Queue<GoldChest>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void SpawnNormalChest(Vector2 _positionToSpawn)
    {
        if (normalChestQueue.Count == 0)
        {
            maxQuantity--;
            NormalChest _newNormalChest = Instantiate(normalChestPrefab, _positionToSpawn, Quaternion.identity);
            normalChestQueue.Enqueue(_newNormalChest);
        }

        normalChestQueue.Dequeue().Born(_positionToSpawn);
    }

    public void SpawnGoldChest(Vector2 _positionToSpawn)
    {
        if (normalChestQueue.Count == 0)
        {
            maxQuantity--;
            NormalChest _newNormalChest = Instantiate(normalChestPrefab, _positionToSpawn, Quaternion.identity);
            normalChestQueue.Enqueue(_newNormalChest);
        }

        normalChestQueue.Dequeue().Born(_positionToSpawn);
    }

    public void BackToBool(AChest oldChest)
    {
        switch (oldChest)
        {
            case NormalChest normalChest:
                if (!normalChestQueue.Contains(normalChest))
                {
                    maxQuantity++;
                    normalChestQueue.Enqueue(normalChest);
                }

                return;
            case GoldChest goldChest:
                if (!goldChestQueue.Contains(goldChest))
                {
                    maxQuantity++;
                    goldChestQueue.Enqueue(goldChest);
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
