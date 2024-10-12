using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingItem : Singleton<PoolingItem>
{
    public int RateDropChest;
    [SerializeField] private BlueGem blueGemPrefab;
    [SerializeField] private BloodBottle bloodBottlePrefab;
    [SerializeField] private PurpleMedicine purpleMedicinePrefab;

    private List<BlueGem> blueGemList = new List<BlueGem>();
    private List<BloodBottle> bloodBottleList = new List<BloodBottle>();
    private List<PurpleMedicine> purpleMedicineList = new List<PurpleMedicine>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void SpawnBlueGem(Vector2 _positionToSpawn)
    {
        if (blueGemList.Count == 0)
        {
            BlueGem _newBlueGem = Instantiate(blueGemPrefab, _positionToSpawn, Quaternion.identity);
            blueGemList.Add(_newBlueGem);
        }

        BlueGem _blueGem = blueGemList[0];
        blueGemList.RemoveAt(0);
        _blueGem.Born(_positionToSpawn);
    }

    public void SpawnBloodBottle(Vector2 _positionToSpawn)
    {
        if (bloodBottleList.Count == 0)
        {
            BloodBottle _newBloodBottle = Instantiate(bloodBottlePrefab, _positionToSpawn, Quaternion.identity);
            bloodBottleList.Add(_newBloodBottle);
        }

        BloodBottle _bloodBottle = bloodBottleList[0];
        bloodBottleList.RemoveAt(0);
        _bloodBottle.Born(_positionToSpawn);
    }

    public void SpawnPurpleMedicine(Vector2 _positionToSpawn)
    {
        if (purpleMedicineList.Count == 0)
        {
            PurpleMedicine _newPurpleMedicine = Instantiate(purpleMedicinePrefab, _positionToSpawn, Quaternion.identity);
            purpleMedicineList.Add(_newPurpleMedicine);
        }

        PurpleMedicine _purpleMedicine = purpleMedicineList[0];
        purpleMedicineList.RemoveAt(0);
        _purpleMedicine.Born(_positionToSpawn);
    }

    public void BackToPool(AItem _item)
    {
        switch (_item)
        {
            case BlueGem _blueGem:
                if (!(blueGemList.Contains(_blueGem)))
                {
                    blueGemList.Add(_blueGem);
                }
                return;

            case BloodBottle _bloodBottle:
                if (!bloodBottleList.Contains(_bloodBottle))
                {
                    bloodBottleList.Add(_bloodBottle);
                }
                return;

            case PurpleMedicine _purpleMedicine:
                if (!purpleMedicineList.Contains(_purpleMedicine))
                {
                    purpleMedicineList.Add(_purpleMedicine);
                }
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
