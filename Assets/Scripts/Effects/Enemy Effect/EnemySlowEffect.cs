public class EnemySlowEffect : AEffect
{
    private AEnemy enemy;

    public float ValueReduceSpeedPercent { get; private set; }
    public float ActiveTime { get; private set; }

    public EnemySlowEffect(EEffectApplied _effectName, float _reduceSpeedPercent, float _activeTime, AEnemy _enemy)
    {
        Id = id;
        enemy = _enemy;
        AppliedEffect = false;
        EffectName = _effectName;
        ValueReduceSpeedPercent = _reduceSpeedPercent;
        ActiveTime = _activeTime;
    }

    public override void ActiveEffect()
    {
        if (!AppliedEffect)
        {
            AppliedEffect = false;
        }
    }

    public void SetValue(float _activeTime, float _percent)
    {
        ActiveTime = _activeTime;
        ValueReduceSpeedPercent = _percent;
    }
}
