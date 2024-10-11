using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private int expToUpgrade;
    [SerializeField] private int step;
    
    public ExpBar expbar;
    public SpellBook spellBook;

    private int currentExp;
    private int countToOpenBook;
    protected override void Awake()
    {
        base.Awake();
        expbar.SetMaxValue(expToUpgrade);
        currentExp = 0;
        expbar.SetCurrentValue(0);
        countToOpenBook = 0;
       // Debug.Log($"{expbar.slider.value}/{expbar.slider.maxValue}");
    }

    public void TakeExp(int _valueExp)
    {
        currentExp += _valueExp;

        //Debug.Log($"{expbar.slider.value}/{expbar.slider.maxValue}");

        if (currentExp >= expToUpgrade)
        {
            currentExp -= expToUpgrade;
            Upgrade();
            return;
        }

        expbar.SetCurrentValue(currentExp);
    }

    public void Upgrade()
    {
        expToUpgrade += step;
        expbar.SetMaxValue(expToUpgrade);
        expbar.SetCurrentValue(currentExp);

        if (++countToOpenBook % 3 == 0)
        {
            spellBook.OpenRedBook();
        }
        else
        {
            spellBook.OpenBlueBook();
        }
    }
}
