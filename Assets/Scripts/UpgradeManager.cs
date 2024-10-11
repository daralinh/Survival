using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private int expToUpgrade;
    [SerializeField] private int step;
    public ExpBar expbar;

    private int currentExp;

    protected override void Awake()
    {
        base.Awake();
        expbar.SetMaxValue(expToUpgrade);
        currentExp = 0;
        expbar.SetCurrentValue(0);
    }

    public void TakeExp(int _valueExp)
    {
        currentExp += _valueExp;

        if (currentExp >= expToUpgrade)
        {
            currentExp -= expToUpgrade;
            Upgrade();
        }

        expbar.SetCurrentValue(currentExp);
    }

    public void Upgrade()
    {
        expToUpgrade += step;
        expbar.SetMaxValue(expToUpgrade);
        expbar.SetCurrentValue(currentExp);
    }
}
