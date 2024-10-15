using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [Header("--- Exp ---")]
    [SerializeField] private int expToUpgrade;
    [SerializeField] private int step;
    [Header("--- buff ---")]
    public float BuffDMG;
    public float BuffAttackSpeed;
    public float DecCoolDownSpell;
    
    public ExpBar expbar;
    public MagicBook magicBook;

    private int currentExp;
    private int countToOpenBook;

    protected override void Awake()
    {
        base.Awake();
        expbar.SetMaxValue(expToUpgrade);
        currentExp = 0;
        expbar.SetCurrentValue(0);
        countToOpenBook = 2;
       // Debug.Log($"{expbar.slider.value}/{expbar.slider.maxValue}");
    }

    public void Start()
    {
        CursorManager.Instance.SetCursor1();
    }

    public void TakeExp(int _valueExp)
    {
        currentExp += _valueExp;
        PoolingTMP.Instance.SpawnTMP($"{_valueExp}", transform.position, EColor.Blue);
        //Debug.Log($"{expbar.slider.value}/{expbar.slider.maxValue}");
        if (currentExp >= expToUpgrade)
        {
            currentExp -= expToUpgrade;
            Upgrade();
            return;
        }

        expbar.SetCurrentValue(currentExp);
    }

    public void RedBookUpgrade()
    {
        magicBook.OpenRedBook();
    } 

    public void Upgrade()
    {
        expToUpgrade += step;
        expbar.SetMaxValue(expToUpgrade);
        expbar.SetCurrentValue(currentExp);

        if (++countToOpenBook % 3 == 0)
        {
            magicBook.OpenRedBook();
        }
        else
        {
            magicBook.OpenBlueBook();
        }
    }
}
