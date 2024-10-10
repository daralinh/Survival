using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingItem : Singleton<PoolingItem>
{
    [SerializeField] private BlueGem blueGemPrefab;
    [SerializeField] private BloodBottle bloodBottlePrefab;

    private List<BlueGem> blueGemList = new List<BlueGem>();
    private List<BloodBottle> bloodBottleList = new List<BloodBottle>();

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

            default:
                return;
        }
    }
}
