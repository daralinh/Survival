using System;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Number: bat, doc, pumpkin, bandit, snake, spider, witch, minotaur")]
    [SerializeField] private int[] lv1;
    [SerializeField] private int[] lv2;
    [SerializeField] private int[] lv3;
    [SerializeField] private int[] lv4;
    [SerializeField] private int[] lv5;
    [SerializeField] private int[] lv6;
    [SerializeField] private int[] lv7;
    [SerializeField] private int[] lv8;

    [SerializeField] private int currentLv;
    private float countTimeToUpLv;

    public int CurrentLv => currentLv;
    public int[,] MaxNumberMonsterPerLevel { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    private void FixedUpdate()
    {
        countTimeToUpLv += Time.fixedDeltaTime;

        if (countTimeToUpLv >= 60)
        {
            SpawnEnemyAroundPlayer.Instance.SpawnDummy();
            LevelUp();
           // Debug.Log(currentLv);
            countTimeToUpLv = 0;
        }
    }

    void Start()
    {
        MaxNumberMonsterPerLevel = new int[9, 9];
        currentLv = Math.Min(8, currentLv);
        currentLv = Math.Max(1, currentLv);

        for (int i = 0; i < 8; i++)
        {
            MaxNumberMonsterPerLevel[1, i] = lv1[i];
        }
        for (int i = 0; i < 8; i++)
        {
            MaxNumberMonsterPerLevel[2, i] = lv2[i];
        }
        for (int i = 0; i < 8; i++)
        {
            MaxNumberMonsterPerLevel[3, i] = lv3[i];
        }
        for (int i = 0; i < 8; i++)
        {
            MaxNumberMonsterPerLevel[4, i] = lv4[i];
        }
        for (int i = 0; i < 8; i++)
        {
            MaxNumberMonsterPerLevel[5, i] = lv5[i];
        }
        for (int i = 0; i < 8; i++)
        {
            MaxNumberMonsterPerLevel[6, i] = lv6[i];
        }
        for (int i = 0; i < 8; i++)
        {
            MaxNumberMonsterPerLevel[7, i] = lv7[i];
        }
        for (int i = 0; i < 8; i++)
        {
            MaxNumberMonsterPerLevel[8, i] = lv8[i];
        }
    }

    public void LevelUp()
    {
        if (++currentLv > 8)
        {
            Debug.Log("kklddf");
            GameManager.Instance.WinGame();
        }

        UpgradeManager.Instance.Upgrade();
    }

    public int GetMaxNumberEnemy(ETag nameEnemy)
    {
        switch (nameEnemy)
        {
            case ETag.Bat:
                return MaxNumberMonsterPerLevel[currentLv, 0];
                
            case ETag.Doc:
                return MaxNumberMonsterPerLevel[currentLv, 1];
                
            case ETag.PumpkinDude:
                return MaxNumberMonsterPerLevel[currentLv, 2];
                
            case ETag.BanditNecromancer:
                return MaxNumberMonsterPerLevel[currentLv, 3];

            case ETag.PinkSnake:
                return MaxNumberMonsterPerLevel[currentLv, 4];

            case ETag.MiniSpider:
                return MaxNumberMonsterPerLevel[currentLv, 5];

            case ETag.Witch:
                return MaxNumberMonsterPerLevel[currentLv, 6];

            case ETag.Minotaur:
                return MaxNumberMonsterPerLevel[currentLv, 7];

            default:
                return 0;
        }
    }
}
