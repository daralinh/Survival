public class PlayerSlowEffect : AEffect
{
    public float ValueReduceSpeedPercent { get; private set; }
    public float ActiveTime { get; private set; }

    public PlayerSlowEffect(EEffectApplied effectName, float reduceSpeedPercent, float activeTime)
    {
        AppliedEffect = false;
        EffectName = effectName;
        ValueReduceSpeedPercent = reduceSpeedPercent;
        ActiveTime = activeTime;
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
