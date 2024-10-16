using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingItem : Singleton<PoolingItem>
{
    public int RateDropChest;
    [SerializeField] private BlueGem blueGemPrefab;
    [SerializeField] private BloodBottle bloodBottlePrefab;
    [SerializeField] private PurpleMedicine purpleMedicinePrefab;
    [SerializeField] private int maxQuantity;

    private Queue<BlueGem> blueGemQueue = new Queue<BlueGem>();
    private Queue<BloodBottle> bloodBottleQueue = new Queue<BloodBottle>();
    private Queue<PurpleMedicine> purpleMedicineQueue = new Queue<PurpleMedicine>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void SpawnBlueGem(Vector2 _positionToSpawn)
    {
        if (maxQuantity < 1)
        {
            return;
        }

        if (blueGemQueue.Count == 0)
        {
            maxQuantity--;
            BlueGem _newBlueGem = Instantiate(blueGemPrefab, _positionToSpawn, Quaternion.identity);
            blueGemQueue.Enqueue(_newBlueGem);
        }

        blueGemQueue.Dequeue().Born(_positionToSpawn);
    }

    public void SpawnBloodBottle(Vector2 _positionToSpawn)
    {
        if (maxQuantity < 1)
        {
            return;
        }

        if (bloodBottleQueue.Count == 0)
        {
            maxQuantity--;
            BloodBottle _newBloodBottle = Instantiate(bloodBottlePrefab, _positionToSpawn, Quaternion.identity);
            bloodBottleQueue.Enqueue(_newBloodBottle);
        }

        bloodBottleQueue.Dequeue().Born(_positionToSpawn);
    }

    public void SpawnPurpleMedicine(Vector2 _positionToSpawn)
    {
        if (maxQuantity < 1)
        {
            return;
        }

        if (purpleMedicineQueue.Count == 0)
        {
            maxQuantity--;
            PurpleMedicine _newPurpleMedicine = Instantiate(purpleMedicinePrefab, _positionToSpawn, Quaternion.identity);
            purpleMedicineQueue.Enqueue(_newPurpleMedicine);
        }

        purpleMedicineQueue.Dequeue().Born(_positionToSpawn);
    }

    public void BackToPool(AItem _item)
    {
        switch (_item)
        {
            case BlueGem _blueGem:
                maxQuantity++;
                blueGemQueue.Enqueue(_blueGem);
                return;

            case BloodBottle _bloodBottle:
                maxQuantity++;
                bloodBottleQueue.Enqueue(_bloodBottle);
                
                return;

            case PurpleMedicine _purpleMedicine:
                maxQuantity++;
                purpleMedicineQueue.Enqueue(_purpleMedicine);
                
                return;

            default:
                return;
        }
    }

    public void DropItem(Vector2 _positionToDrop)
    {
        int randomValueToDropChest = Random.Range(1, 100);

        if (randomValueToDropChest <= RateDropChest)
        {
            PoolingChest.Instance.DropChest(_positionToDrop);
            return;
        }

        if (PlayerController.Instance.HpManager.IsFullHp())
        {
            PoolingItem.Instance.SpawnBlueGem(_positionToDrop);
        }
        else
        {
            int randomValue = Random.Range(1, 100);

            if (randomValue < 21 && randomValue > 1)
            {
                PoolingItem.Instance.SpawnBloodBottle(_positionToDrop);
                return;
            }

            if (randomValue == 1)
            {
                PoolingItem.Instance.SpawnPurpleMedicine(_positionToDrop);
                return;
            }

            PoolingItem.Instance.SpawnBlueGem(_positionToDrop);
        }
    }
}
